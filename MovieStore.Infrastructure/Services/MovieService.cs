using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        // Constructor Injection, inject movieRepository instance
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            return await _movieRepository.GetHighestRevenueMovies();
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()
        {
            //return await _movieRepository.GetHighestRatedMovies();
            //int x = 0;
            //int z = 4 / x;
            var movies = await _movieRepository.GetHighestRatedMovies();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            return await _movieRepository.GetMoviesByGenre(genreId);
        }

        public async Task<IEnumerable<Cast>> GetCastsByMovieId(int movieId)
        {
            return await _movieRepository.GetCastsByMovie(movieId);
        }

        public async Task<IEnumerable<MovieCast>> GetMovieCastsByMovieId(int movieId)
        {
            return await _movieRepository.GetMovieCastsByMovie(movieId);
        }

        public async Task<decimal> GetMovieRatingById(int movieId)
        {
            return await _movieRepository.GetMovieRating(movieId);
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<int> GetMoviesCount(string title = "")
        {
            return await _movieRepository.GetCountAsync(p => p.Title == title);
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            return await _movieRepository.AddAsync(movie);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {

            return await _movieRepository.UpdateAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetMovieByUserId(int userId)
        {
            return await _movieRepository.GetMoviesByUser(userId);
        }
    }

    //public class MovieServiceTest : IMovieService
    //{
    //    private readonly IMovieRepository _movieRepository;

    //    // Constructor Injection, inject movieRepository instance
    //    public MovieServiceTest(IMovieRepository movieRepository)
    //    {
    //        _movieRepository = movieRepository;
    //    }

    //    public Task<Movie> CreateMovie(Movie movie)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Movie> GetMovieById(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> GetMoviesCount(string title = "")
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
    //    {
    //        return await _movieRepository.GetHighestRevenueMovies();
    //    }

    //    public Task<IEnumerable<Movie>> GetTop25RatedMovies()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Movie> UpdateMovie(Movie movie)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
