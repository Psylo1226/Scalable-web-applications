using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostDbContext _context;

        public PostController(PostDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetPostWithComments(int id)
        {
            var post = _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost("addPost")]
        public IActionResult CreatePost([FromBody] Post feedPost, [FromServices] IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient();

            var post = new Post
            {
                Title = feedPost.Title,
                Content = feedPost.Content,
                CreatedAt = DateTime.UtcNow,
                Likes = 0,
                UserId = feedPost.UserId
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPostWithComments), new { id = post.Id }, post);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
