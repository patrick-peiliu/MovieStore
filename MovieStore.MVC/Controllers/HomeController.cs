using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.Core.Entities;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    // Any c# class can become a MVC Controller if it inherits from
    // Controller base Class from Microsoft.AspNetCore

    // GET https://localhost:2323/Home/Index

    // Routing
    // HomeController
    // Index -- Action Method

    public class HomeController : Controller
    {
        //public JsonResult Index()
        //{
        //    return Json(new { id = 1, name = "Patrick" });
        //}
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // Action method
        public async Task<IActionResult> Index()
        {
            // we need the MovieCard in a lot of pages:
            // 1. Home page - show top revenue movies -> Movie Card
            // 2. Genres / show movies belonging to that genre -> Movie Card
            // 3. Top Rated Movie -- Top Movie as a card
            // we have to create the Movie card in a reuseable way
            // partial views will help us in creating reusable Views across the app
            // partial views have to be inside another view, named starts with _
            // it cannot be call directly using url
            // _MovieCard Partial View is used inside index View


            //var movies = await _movieService.GetTop25HighestRevenueMovies();
            var movies = await _movieService.GetTop25RatedMovies();
            return View(movies);
            // 0 and null
            // int y;
            // value type, default val is null

            // int? x
            // nullable

            // 1. Program.cs -> main method
            // 2.Startup calss
            // 3.Configuration services
            // 4.Configure
            // 5.HomeController
            // 6.Action Method
            // 7.Return a view

            // return instance of a class that implements IActionResult interface
            // default MVC will look for same View name as Action method name
            // it will look inside Views folder -> Home (same name as Controller) -> Index.cshtml


            // Middleware - software executed logic -> make sure it functions properly that can call the next middleware
            // dc -> bos
            // M1 -> M2 -> M3 -> M4 ->response           
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        //public string Index()
        //{
        //    return "ABC";
        //}
    }

    //public interface XYZ
    //{
    //    public int Add(int a, int b);
    //}

    //public interface ABC
    //{
    //    public int Multiply(int a, int b);
    //}

    //public class MyClass: XYZ, ABC
    //{
    //    public int Add(int a, int b)
    //    {
    //        return a + b;
    //    }

    //    public int Multiply(int a, int b)
    //    {
    //        return a * b;
    //    }
    //}
}
