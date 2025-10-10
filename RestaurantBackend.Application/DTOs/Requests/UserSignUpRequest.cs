using RestaurantBackend.Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Requests
{
    public class UserSignUpRequest
    {
        [Required(ErrorMessage = "Name field can't be empty")]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender can't be empty")]
        public Gender Gender { get; set; }

        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Email field can't be empty")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone can't be empty")]
        [RegularExpression(@"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}", ErrorMessage = "Phone must match +7 (xxx) xxx-xx-xx")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Password field can't be empty")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;
    }
}