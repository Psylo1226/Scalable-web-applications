using Microsoft.AspNetCore.Mvc;
using FaceSpace.Models;

namespace FaceSpace.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserProfileViewModel(); // Tworzenie modelu
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

            // Jeśli coś poszło nie tak, odtwórz listę awatarów
            model.AvatarOptions = new List<string>
            {
                "/img/avatars/avatar1.png",
                "/img/avatars/avatar2.png",
                "/img/avatars/avatar3.png",
                "/img/avatars/avatar4.png"
            };

            return View(model);
        }
    }
}
