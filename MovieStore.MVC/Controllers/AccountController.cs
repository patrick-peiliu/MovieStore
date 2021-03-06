﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            // srv side Validation checks
            // check the input model whether it meets the data annotation requirements
            if (ModelState.IsValid)
            {
                // now call the service only when it's valid
                var createdUser = await _userService.RegisterUser(userRegisterRequestModel);
                return RedirectToAction("Login");
            }
            // we take this object from the View
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestModel loginRequest)
        {
            // Http is stateless, each request is independent of each other
            // 10:00 AM user 1 -> Https://localhost/movies/index
            // 10:00 AM user 2 -> Https://localhost/movies/index
            // 10:00 AM user 3 -> Https://localhost/movies/index
            // 10:01 AM user 1 -> Https://localhost/account/login
            // create an authenticate cookie to check whether the user is logged in
            // cookie is a way of storing information in browser, localstorage and sessionstorage
            // check if any cookie present then it will be automatically sent to the srv
            // time to expired may vary based on specific application, like banks or facebook

            // 10:02 AM user 1 -> Https://localhost/user/purchases
            // we are expecting a page that shows movies bought by user 1
            // we need to check whether the cookie is valid or not and whether it's expired or not
            // *** cookie is generated by the browser ***
            // *** cookie is one way of state management at the client side ***

            // 10:01 AM user 2 -> Https://localhost/account/login

            if (ModelState.IsValid)
            {
                // call service layer to validate user
                var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
                // we want to show First Name, Last Name on header (navigation)
                // do not show secret information
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
                // create Claims based on application needs
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                };

                // we need to create an Identity Object to hold these claims
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Finally we are going to create a cookie that will be attached to the http response
                // ---------------------------------------------------
                //                 **Important**
                // httpContext is the most important class in ASP.NET
                // which holds all the information regarding that http request/response
                // ---------------------------------------------------

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // manually creating cookie
                // HttpContext.Response.Cookies.Append("userLanguage", "English");

                // Once ASP.NET Creates Authentication cookies, it will check for that cookie in the HttpRequest
                // and see if the cookie is not expired or valid
                // it will decrypt the information present in the cookie to check whether User is Authenticated
                // it will also get claims from the cookies

                // redirect
                return LocalRedirect("~/");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");         
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> Index(LoginRequestModel loginRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // call service layer to validate user
        //        var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid Login");
        //        }
        //    }
        //    return View();
        //}
    }
}