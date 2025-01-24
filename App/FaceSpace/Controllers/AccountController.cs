using Microsoft.AspNetCore.Mvc;

namespace FaceSpace.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // Usuń ciasteczka i wyczyść sesję
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("Username");
            HttpContext.Session.Clear();

            // Przekierowanie na stronę główną
            return RedirectToAction("Index", "Home");
        }
    }
}
