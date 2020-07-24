using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieStore.Core.Entities
{ 
    public class MovieGenre
    {
        // both for PK & FK
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // many to many relationship need this junction table

        // navigation properties
        public virtual Movie Movie { get; set; }
        public virtual Genre Genre { get; set; }
    }
}