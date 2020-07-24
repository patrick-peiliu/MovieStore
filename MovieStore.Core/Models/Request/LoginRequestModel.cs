﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Core.Models.Request
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
