using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Views.Shared.Components.Genres
{
    public class GenresViewComponent : ViewComponent
    // new tools developed by MS
    {
        private readonly IGenreService _genreService;

        public GenresViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        // controller method returns Task<IActionResult>
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }
    }
}
