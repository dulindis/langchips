using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.DTOs
{
    public class UserDTO
    {

        [Required(ErrorMessage = "First Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]

        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } 

        [Required,EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? Phone { get; set; }


        // [Required(ErrorMessage = "Username is required.")]

    }
}
