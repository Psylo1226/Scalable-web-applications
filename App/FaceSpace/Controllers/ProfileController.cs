using Microsoft.AspNetCore.Mvc;
using FaceSpace.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FaceSpace.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
