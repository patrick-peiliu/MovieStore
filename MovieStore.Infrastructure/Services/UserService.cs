using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IMovieService _movieService;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;


        public UserService(IUserRepository userRepository, ICryptoService cryptoService,
            IMovieService movieService, IPurchaseRepository purchaseRepository,
            IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _movieService = movieService;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> CheckFavorite(int userId, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(p => p.UserId == userId
                                                                && p.MovieId == movieId);
        }

        public async Task<bool> CheckPurchase(int userId, int movieId)
        {
            return await _purchaseRepository.GetExistsAsync(p => p.UserId == userId
                                                                && p.MovieId == movieId);
        }

        public async Task<Favorite> Favorite(FavoriteRequestModel favoriteRequestModel)
        {
            var movie = await _movieService.GetMovieById(favoriteRequestModel.MovieId);
            var favorite = new Favorite
            {
                UserId = favoriteRequestModel.UserId,
                MovieId = favoriteRequestModel.MovieId
            };

            return await _favoriteRepository.AddAsync(favorite);
        }


        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            var favorite = await _favoriteRepository.ListAsync(f => f.UserId == favoriteRequestModel.UserId
                                                                    && f.MovieId == favoriteRequestModel.MovieId);
            
            await _favoriteRepository.DeleteAsync(favorite.First());
        }


        public async Task<Review> Review(ReviewRequestModel reviewRequestModel)
        {
            var review = new Review
            {
                UserId = reviewRequestModel.UserId,
                MovieId = reviewRequestModel.MovieId,
                Rating = reviewRequestModel.Rating,
                ReviewText = reviewRequestModel.ReviewText
            };

            return await _reviewRepository.AddAsync(review);
        }


        public async Task<IEnumerable<Review>> GetReviewsByUserId(int userId)
        {
            return await _reviewRepository.GetReviewsByUser(userId);
        }

        public async Task<Purchase> Purchase(PurchaseRequestModel purchaseRequestModel)
        {
            var movie = await _movieService.GetMovieById(purchaseRequestModel.MovieId);

            var purchase = new Purchase
            {
                UserId = purchaseRequestModel.UserId,
                PurchaseNumber = purchaseRequestModel.PurchaseNumber.Value,
                TotalPrice = movie.Price.Value,
                MovieId = purchaseRequestModel.MovieId,
                PurchaseDateTime = purchaseRequestModel.PurchaseDateTime.Value
            };

            return await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Step 1 : Check whether this user already exists in the db
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if(dbUser != null)
            {
                // we already have this user(email) in our table
                // return or throw an exception saying Conflict user
                throw new Exception("User already registered, please try to login");
            }

            // Step 2 : Let's create a random unique salt
            var salt = _cryptoService.GenerateSalt();

            // **** Never ever create your own hashing algorithm, always use Industry standard algorithm***
            // Step 3 : We hash the pw with the salt created in the above step

            var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);

            // create User object so that we can save it to User Table
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };

            // Step 4: Save it to Database
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return response;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // Step 1: Get user record from the db by email
            var user = await _userRepository.GetUserByEmail(email);

            if(user == null)
            {
                // user does not exist
                throw new Exception("Register first, the user does not exist.");
            }

            // Step 2: Hash the pw that user entered in the page with salt in db
            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);

            // Step 3: Compare the db hashed password with the Hashed pw generated in Step 2

            if(hashedPassword == user.HashedPassword)
            {
                // user entered the right password
                // send some user details
                var response = new UserLoginResponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email
                };

                return response;
            }

            return null;
        }
    }
}
