using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Services
{
    public class GenreService:IGenreService
    {
        // In our application there might be some data that we get from db that won't change often
        // for example -- genres, in our application they dont' change at all.
        // we display gneres in our navi header
        // genreService will call genreRepository
        // that will call genre table --> database call
        // next time when you try to get genres you may check the cache has that info or not
        // if yes, you can take it from cache instead of going to database

        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private static readonly string _genresKey = "genres";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromDays(30);

        // Constructor Injection, inject genreRepository instance
        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            // 1. Check if the cache has genres by key
            var genres = await _memoryCache.GetOrCreateAsync(_genresKey, async entry =>
            {
                // check the time span
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _genreRepository.ListAllAsync();
            });

            return genres;
            //return await _genreRepository.ListAllAsync();
        }
    }
}
