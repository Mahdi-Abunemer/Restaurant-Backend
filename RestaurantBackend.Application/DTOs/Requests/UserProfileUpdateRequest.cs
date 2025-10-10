using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Requests
{
    public class UserProfileUpdateRequest
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
