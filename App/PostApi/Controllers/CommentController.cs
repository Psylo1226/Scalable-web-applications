using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.Models;

namespace PostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly PostDbContext _context;

        public CommentController(PostDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            var postExists = _context.Posts.Any(p => p.Id == comment.PostId);
            if (!postExists)
            {
                return BadRequest(new { Message = "Post not found" });
            }

            comment.CreatedAt = DateTime.UtcNow;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return Ok(comment);
        }
    }

}
