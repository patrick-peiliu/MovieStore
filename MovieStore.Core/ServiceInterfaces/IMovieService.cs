using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.ServiceInterfaces
{
    // Designed for loosely coupled code
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies();

        // join review and movie table
        Task<IEnumerable<Movie>> GetTop25RatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);
        Task<IEnumerable<Cast>> GetCastsByMovieId(int movieId);
        Task<IEnumerable<MovieCast>> GetMovieCastsByMovieId(int movieId);
        Task<IEnumerable<Movie>> GetMovieByUserId(int userId);

        Task<decimal> GetMovieRatingById(int movieId);
        Task<Movie> GetMovieById(int id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<int> GetMoviesCount(string title = "");
    }
}
