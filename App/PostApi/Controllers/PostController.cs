using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.Models;

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

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
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
