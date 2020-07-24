using System;
using System.Collections.Generic;

namespace MovieStore.Core.Models.Request
{
    public class PurchaseRequestModel
    {
        public PurchaseRequestModel()
        {
            PurchaseDateTime = DateTime.UtcNow;
            PurchaseNumber = Guid.NewGuid();
        }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public Guid? PurchaseNumber { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? PurchaseDateTime { get; set; }
    }
}
