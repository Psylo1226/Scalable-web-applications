using System.Diagnostics;
using FaceSpace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FaceSpace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Feed()
        {
            // Initialize the FeedModel with sample posts
            var model = new FeedModel
            {
                Posts = new List<FeedPost>
                {
                    new FeedPost
                    {
                        Author = "John Doe",
                        Content = "This is a sample post content. It can contain any text or links.",
                        Timestamp = DateTime.Now.AddMinutes(-5),
                        Likes = 10,
                        Comments = new List<FeedComment>
                        {
                            new FeedComment { Author = "Jane Smith", Text = "This is a great post!" },
                            new FeedComment { Author = "Mark Johnson", Text = "Totally agree with you!" }
                        }
                    },
                    new FeedPost
                    {
                        Author = "Alice Brown",
                        Content = "Here's another post for the feed!",
                        Timestamp = DateTime.Now.AddHours(-1),
                        Likes = 5,
                        Comments = new List<FeedComment>
                        {
                            new FeedComment { Author = "John Doe", Text = "I love this!" }
                        }
                    }
                }
            };

            return View(model); // Pass the model to the view
        }
    }
}