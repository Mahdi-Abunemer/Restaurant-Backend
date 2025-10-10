using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _cfg;
        private readonly byte[] _key;

        public TokenService(IConfiguration cfg)
        {
            _cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
            _key = Encoding.UTF8.GetBytes(_cfg["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured"));
        }

        public AuthResponse GenerateToken(AuthUserInfo user)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(int.Parse(_cfg["Jwt:ExpiresMinutes"] ?? "60"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            var access = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthResponse { Token = access };
        }

    }
}
