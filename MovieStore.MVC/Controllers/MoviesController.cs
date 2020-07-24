using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieStore.MVC.Filters;

namespace MovieStore.MVC.Controllers
{
    public class Test
    {
        public int Id { get; set; }
    }

    public class MoviesController : Controller
    {
        // IOC container, Asp.net core has built-in IOC/DI
        // In .Net Framework we need to rely on third-party IOC to do Dependency Injection
        // like Autofac, Ninject
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public MoviesController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Genres(int id)
        {
            var movies = await _movieService.GetMoviesByGenreId(id);
            return View(movies);
        }

        [MovieStoreFilter]
        // GET: localhost/Movies/details/?id
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieById(id);

            var userClaim = HttpContext.User.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            bool purchased = false;
            bool favorited = false;
            if ( userClaim != null && !string.IsNullOrWhiteSpace(userClaim.Value))
            {
                purchased = await _userService.CheckPurchase(Int32.Parse(userClaim.Value), id);
                favorited = await _userService.CheckFavorite(Int32.Parse(userClaim.Value), id);
            }

            var checkedMovie = new MovieDetailsModel
            {
                Movie = movie,
                PurchaseFlag = purchased,
                FavoriteFlag = favorited
            };

            return View(checkedMovie);
        }

        // GET: localhost/Movies/index
        // POST: 
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        //public async Task<IActionResult> Index()
        {
            // call our movie service, method, highest grossing method
            //var movies = await _movieService.GetTop25HighestRevenueMovies();
            //var movies = await _movieService.GetMovieById(id);
            //int id = 2;
            var movies = await _movieService.GetMovieById(id);

            return View(movies);

            // Thread1
            // var movieService = new MovieService();
            // 5 seconds var movies = await movieService.GetALlMovie(); i/0
            // return a Task for you

            // **** WHY async is important ****
            // Improving the scalability of the application
            // so that your application can serve many concurrent requests properly
            // async/await will prevent thread starvation scenario.
            // because no threads are going back in pools instead of waiting for I/O operations
            // ++++ I/O bound operation, ------ not for CPU operations
            // async is good for Database calls, Http calls, all related things over the network
            // return a Task<Movie>, Task<int>
            // in your C# or any Library whenever you see a method with Async in the method name
            // that means you can await that method
            // EF, two kind of methods, normal sync method, async methods...
            // .NET 4.5, C# 5 year 2012


            //var movies = new List<Movie>
            //{
            //    new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
            //    new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
            //    new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
            //    new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
            //    new Movie {Id = 5, Title = "Inception", Budget = 1200000},
            //    new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
            //    new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
            //    new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
            //};

            // Data is passed from Controller action method to the View
            // Usually it's preferred to send a strongly typed Model or obj to the View


            // Three ways to send data from Controller to View:
            //      1. Stongly-typed models (preferred way)
            //      2. ViewBag -- dynamic
            //      3. ViewData - key/value

            // dynamic type
            // send some extra data not in model
            // however prefer strongly type
            // ViewBag
            // ViewBag.MoviesCount = movies.Count;
            // ViewData["myname"] = "King";
            // complie time checks vs run-time checks
            /*
                The static type checks are made at compile time.
                That means that you cannot compile the application, if you try to assign a string to int.
                Run-time on the other hand refers to the time when the code is actually executed.
                For example exceptions are always thrown at run-time.
             */


            // go to db and get list of movies and give it to the view
            //return View(movies);
            //return View();
        }

        [HttpPost]
        public IActionResult Create(string title, decimal budget)
        {
            // POSThttp://localhost/Movies/create

            // model binding
            // look at incoming request and maps the input elements name/val
            // with the para names of the action method
            // then the para will have the val automatically
            // also performs casting / converting

            // we need to get the data from html view and save it in db
            return View();

        }

        [HttpGet]
        public IActionResult Create()
        {
            // GEThttp://localhost/Movies/index

            // we need to have this method so that we can show the empty page for
            // user to enter Movie information that needs to be created
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var movies = await _movieService.GetMovieById(id);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Rating()
        {
            var movies = await _movieService.GetTop25RatedMovies();
            return View(movies);
        }
    }


}