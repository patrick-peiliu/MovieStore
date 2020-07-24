using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class UserRole
    {
        // many to many relationship
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // navigation properties
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
