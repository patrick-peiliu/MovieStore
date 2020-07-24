using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // FK : Movie + Id
        public int MovieId { get; set; }

        // one to many relationship

        // If someone gave your Trailer ID and you wanna find
        // movie details, then this property will be useful
        // Navigation property for extra info for movie
        // obj creation by c# for getting info

        // one trailer belongs to single movie
        public Movie Movie { get; set; }

        // while one movie can have multiple trailers
    }
}