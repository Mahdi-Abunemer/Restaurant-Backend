using RestaurantBackend.Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Responses
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Address { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
