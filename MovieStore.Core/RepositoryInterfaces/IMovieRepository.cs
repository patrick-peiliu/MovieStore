using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<IEnumerable<Movie>> GetHighestRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
        Task<IEnumerable<Movie>> GetMoviesByUser(int userId);
        Task<IEnumerable<Cast>> GetCastsByMovie(int movieId);
        Task<IEnumerable<MovieCast>> GetMovieCastsByMovie(int movieId);
        Task<decimal> GetMovieRating(int movieId);
    }

    // IAsyncRepository has 8 methods
    // we have a class MovieRepo implements IMovieRepository
    // which need to implement 9 methods
    // we have a class MovieRepo implements IMovieRepository and inherit EfRepo
}
