using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                                        .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                                        .Include(m => m.MovieGenres).ThenInclude(m => m.Genre)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return null;

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).AverageAsync(r => r.Rating);

            if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }

        // get all purchased movies
        public async Task<IEnumerable<Movie>> GetMoviesByUser(int userId)
        {
            var movies = await (from p in _dbContext.Purchases.Where(p => p.UserId == userId)
                                join m in _dbContext.Movies
                                on p.MovieId equals m.Id
                                select m).ToListAsync();

            return movies;
        }

        public async Task<decimal> GetMovieRating(int movieId)
        {
            var rating = await _dbContext.Reviews.Where(m => m.MovieId == movieId).AverageAsync(r => r.Rating);
            // var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).AverageAsync(r => r.Rating);
            return rating;
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue)
                .Take(25).ToListAsync();
            // sql expression
            // select top 25 from Movies order by Revenue desc
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = await _dbContext.Movies.Where(m => m.MovieGenres.Any(g => g.GenreId == genreId)).ToListAsync();
            return movies;
        }


        public async Task<IEnumerable<Cast>> GetCastsByMovie(int movieId)
        {
            var casts = await _dbContext.Casts.Where(c => c.MovieCasts.Any(m => m.MovieId == movieId)).ToListAsync();
            return casts;
        }


        public async Task<IEnumerable<MovieCast>> GetMovieCastsByMovie(int movieId)
        {
            var movieCasts = await _dbContext.MovieCasts.Where(mc => mc.MovieId == movieId).ToListAsync();
            return movieCasts;
        }

        public async Task<IEnumerable<Movie>> GetHighestRatedMovies()
        {
            //var movies = await (from m in _dbContext.Movies
            //                    join r in _dbContext.Reviews
            //                    on m.Id equals r.MovieId
            //                    orderby r.Rating descending
            //                    select new { Name = m.Title, Rate = r.Rating }).Take(25).ToListAsync();

            //return (IEnumerable<Movie>) movies;
            var movies = await _dbContext.Movies.OrderByDescending
                    (r => r.Reviews.Average(rt => rt.Rating)).Take(25).ToListAsync();

            //var test = await _dbContext.Reviews.Include(m => m.Movie).GroupBy
            //            (r => { r.MovieId, Postyer}).OrderByDescending
            // (g => g.Average(t => t.Rating).Select( m=> new Movies {
            // id = m.key.id,
            // Rating = m.Average(x => x.Rating)
            // }).Take(25).ToListAsync();

            /*
              dbcontext.Reviews.Include(m => m.Moview).GrouoBy(
                r => {MovieId, Postyer}.orderBydescding( g => g.Average(m => m.Rating)))
                .Select( m => new Movie {
                id = m.key.id,
                Rating = m.Averahe(x => x.rating)
                 Take(20)
                 TolIstAsync()
            })
             */


            return movies;
        }
    }
}
