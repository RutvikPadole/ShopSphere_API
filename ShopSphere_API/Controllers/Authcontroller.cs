using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopSphere_API.Data;
using ShopSphere_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopSphere_API.Controllers
{
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
        
        public IActionResult Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.Users.Add (user);
            _context.SaveChanges();
            return Ok("User Registred");
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
