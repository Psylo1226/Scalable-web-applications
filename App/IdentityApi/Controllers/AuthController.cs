using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists");
            }

            if (string.IsNullOrEmpty(user.Email)) 
            {
                return BadRequest("Email is required");
            }

            try
            {
                MailAddress m = new MailAddress(user.Email);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Email address");
            }

            user.PasswordHash = HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Username and password are required.");
            }

            var dbUser = _context.Users
                .FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == HashPassword(user.PasswordHash));

            if (dbUser == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok("Login successful");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
