using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopSphere_API.Data;
using ShopSphere_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShopSphere_API.DTOs;

namespace ShopSphere_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Authcontroller : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public Authcontroller (AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }

        [HttpPost ("Login")]

        public IActionResult Login (User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(X => X.Email == loginUser.Email);

            if (user == null)
                return Unauthorized();

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password);

            if(!isPasswordValid)
                return Unauthorized();

            var token = GenerateToken(user);
            return Ok(token);
        }

        private string GenerateToken (User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: _config["Jwt:Isuuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                 signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
