using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Auth
{
    public record AuthUserInfo(Guid Id, string Name, string Email);
}
