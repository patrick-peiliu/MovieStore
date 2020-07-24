using System;
using System.Collections.Generic;
using MovieStore.Core.Entities;

namespace MovieStore.Core.Models.Response
{
    public class MovieDetailsModel
    {
        public bool PurchaseFlag { get; set; }
        public bool FavoriteFlag { get; set; }
        public Movie Movie { get; set; }
    }
}
