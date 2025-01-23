using Microsoft.AspNetCore.Mvc;
using FaceSpace.Models;

namespace FaceSpace.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserProfileViewModel
            {
                Name = "John Doe",
                Description = "Welcome to your profile!",
                SelectedAvatar = "/img/avatar1.png",
                AvatarOptions = new List<string>
                {
                    "/img/avatar1.png",
                    "/img/avatar2.png",
                    "/img/avatar3.png",
                    "/img/avatar4.png"
                },
                Friends = new List<Friend>
                {
                    new Friend { Name = "James Pittman", Avatar = "/img/avatar2.png" },
                    new Friend { Name = "Ella Cabena", Avatar = "/img/avatar3.png" }
                },
                SuggestedFriends = new List<Friend>
                {
                    new Friend { Name = "Mitchell Ashcroft", Avatar = "/img/avatar4.png" },
                    new Friend { Name = "Laura Pollock", Avatar = "/img/avatar2.png" }
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Możesz tutaj zapisać zmiany do bazy danych
                TempData["SuccessMessage"] = "Profile updated successfully!";
            }

            model.AvatarOptions = new List<string>
            {
                "/img/avatar1.png",
                "/img/avatar2.png",
                "/img/avatar3.png",
                "/img/avatar4.png"
            };

            return View(model);
        }
    }
}
