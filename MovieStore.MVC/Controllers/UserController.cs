using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

        // Filters in ASP.NET
        // the snippets runs before a controller or action method executions or when some events happens
        // or after specific stages in the http pipeline
        // 1. Authorization
        // 2. Action Filter
        // 3. Results Filter
        // 4. Exception Filter, but in real world we user Exception Middleware to catch exceptions
        // 5. Resource Filter


        // 1. Purchase a Movie, httpPost, store that info in the Purchase table
        // https://localhost/User/Purchase --[HttpPost]
        // first check whether the user already bought that movie
        // Buy button in the Movie Details page will call the above method
        // if user already bought that movie, then replace Buy button with Watch Movie button

        [HttpPost]
        // The action method will be executed only when authorization is true
        public async Task<IActionResult> Purchase(PurchaseRequestModel purchaseRequestModel)
        {
                purchaseRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var movie = await _movieService.GetMovieById(purchaseRequestModel.MovieId);

                var moviePurchased = await _userService.Purchase(purchaseRequestModel);
                return LocalRedirect("~/User/Purchases");
        }

        // purchase repo : findbyID firstordefault purchase user id
        // purchasemovieservice: purchaseMovie()
        // add a purchase obj
        // save it to db using addsync()

        // 2. Get all the Movies Purchased by user, loged-in user
        // take userId from httpcontext and get all the movies
        // and give them to Movie Card partial View
        // https://localhost/User/Purchase --[HttpGet]

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var movies = await _movieService.GetMovieByUserId(userId);

            return View("/Views/Movies/Genres.cshtml", movies);
        }

        // 3. Create a Review for a Movie for Loged In user
        // take userid from HttpContext and save info in Review Table
        // https://localhost/User/Review --[HttpPost]
        // Review button will open a popup
        // asks user to enter a small review in textarea and
        // have him enter movie rating between 1 and 10 and then save

        [HttpPost]
        public async Task<IActionResult> Review(ReviewRequestModel reviewRequestModel)
        {
            reviewRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var movie = await _movieService.GetMovieById(reviewRequestModel.MovieId);

            var movieReview = await _userService.Review(reviewRequestModel);
            
            return LocalRedirect("~/User/Reviews");
        }

        // 4. Get all the Reviews done my loged in User,
        // http:localhost:12112/User/reviews -- HttpGet
        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var reviews = await _userService.GetReviewsByUserId(userId);

            return View("/Views/Movies/Index.cshtml", reviews);
        }

        // 5. Add a Favorite Movie for Loged In User
        // http:localhost:12112/User/Favorite -- HttpPost
        // Add another button called Favorite same concept as Purchase
        // Use fontawesome library for button icons https://fontawesome.com/
        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel favoriteRequestModel)
        {
            favoriteRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var movie = await _movieService.GetMovieById(favoriteRequestModel.MovieId);
            var movieFavorited = await _userService.Favorite(favoriteRequestModel);
            return LocalRedirect("~/");
        }

        // 6. Check if a particular Movie has been added as Favorite by logedin user
        // http:localhost:12112/User/{123}/movie/{12}/favorite  HttpGet
        [HttpGet("/User/{userId}/Movie/{movieId}/Favorite")]
        public async Task<IActionResult> CheckFavorite(int userId, int movieId)
        {
            var flag = await _userService.CheckFavorite(userId, movieId);

            return View("/Views/Movies/Create.cshtml", flag);
        }

        // 7. Remove favorite
        // http:localhost:12112/User/Favorite -- Httpdelete
        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            favoriteRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var movie = await _movieService.GetMovieById(favoriteRequestModel.MovieId);
            await _userService.RemoveFavorite(favoriteRequestModel);
            return LocalRedirect("~/");
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}