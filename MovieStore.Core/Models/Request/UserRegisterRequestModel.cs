using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        // Model folder is used for UI
        // Data Annotations are useful for validation
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Make sure password length is between 8 and 20", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        // password should be strong
        // one capital letter, lower letter, a number and a special character
        public string Password { get; set; }

    }
}
