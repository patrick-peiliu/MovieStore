using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> ValidateUser(string email, string password);
        Task<Purchase> Purchase(PurchaseRequestModel purchaseRequestModel);
        Task<bool> CheckPurchase(int userId, int movieId);
        Task<Favorite> Favorite(FavoriteRequestModel favoriteRequestModel);
        Task<bool> CheckFavorite(int userId, int movieId);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequestModel);
        Task<Review> Review(ReviewRequestModel reviewRequestModel);
        Task<IEnumerable<Review>> GetReviewsByUserId(int userId);

    }
}
