using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantBackend.Application.Interfaces.Services;
using RestaurantBackend.Infrastructure.Data;
using RestaurantBackend.Infrastructure.Identity;
using RestaurantBackend.Infrastructure.Services;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();

//Jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateAudience = true,
          ValidateIssuer = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidAudience = builder.Configuration["Jwt:Audience"],
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
      };
  });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();

