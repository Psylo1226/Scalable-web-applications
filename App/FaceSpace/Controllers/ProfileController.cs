using Microsoft.AspNetCore.Mvc;
using FaceSpace.Models;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FaceSpace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:5002/api/people/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { Message = "Failed to load profile." });
            }

            var userProfile = await response.Content.ReadFromJsonAsync<UserProfile>();

            var viewModel = new UserProfileViewModel
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Description = userProfile.Description,
                SelectedAvatar = string.IsNullOrEmpty(userProfile.AvatarUrl) ? "/img/avatar1.png" : userProfile.AvatarUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userProfile = new UserProfile
            {
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                AvatarUrl = model.SelectedAvatar
            };

            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(userProfile),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"https://localhost:5002/api/people/{userId}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Update failed: {errorMessage}");
                return View("Error", new ErrorViewModel { Message = "Failed to update profile." });
            }

            return RedirectToAction("Index");
        }

    }
}
