using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, [FromServices] IHttpClientFactory httpClientFactory)
            {
            if (_userRepository.GetUserByUsername(user.Username) != null)
            {
                return BadRequest(new { Message = "User already exists" });
            }

            user.PasswordHash = HashPassword(user.PasswordHash);
            _userRepository.AddUser(user);

            var httpClient = httpClientFactory.CreateClient();
            var defaultProfile = new
            {

                UserId = user.Id.ToString(), // Powiązanie profilu z użytkownikiem
                FirstName = "",
                LastName = "",
                Description = "New user",
                AvatarUrl = ""
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:5002/api/people", defaultProfile);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, new { Message = "User created, but failed to create user profile." });
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userRepository.GetUserByUsername(request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            // Ciasteczko z UserId
            Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            // Ciasteczko z Username
            Response.Cookies.Append("Username", user.Username, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return Ok(new { Message = "Login successful" });
        }

        [HttpGet("session-status")]
        public IActionResult GetSessionStatus()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                return Ok(new { IsAuthenticated = true, UserId = userId });
            }

            return Ok(new { IsAuthenticated = false });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
