using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Requests
{
    public class UserSignInRequest
    {
        [Required(ErrorMessage = "Email field can't be empty")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field can't be empty")]
        public string Password { get; set; } = null!;
    }
}
