using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Movie
    {
        // use fluent API

        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        public string Tagline { get; set; }
        public string TmdbUrl { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        // we don't want to Create Rating Column
        // but we want C# rating property in our Entity so that we can show Movie rating in the UI
        public decimal? Rating { get; set; }

        // one movie can have multiple trailers
        public ICollection<Trailer> Trailers { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}