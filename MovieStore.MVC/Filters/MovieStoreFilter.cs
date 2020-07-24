using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieStore.MVC.Filters
{
    public class MovieStoreFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // will execute this method after action method executes

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var data = context.HttpContext.Request.Path;
            var authenticated = context.HttpContext.User.Identity.IsAuthenticated;

            // we want track the info of how many people came to the movie details page
            // will execute this method before action method executes
        }
    }
}
