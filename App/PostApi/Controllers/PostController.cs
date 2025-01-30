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
        [HttpGet("user/{userId}")]
        public IActionResult GetUserPosts(string userId)
        {
            var posts = _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            return Ok(posts);
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
        [HttpPut("like/{id}")]
        public IActionResult ToggleLike(int id, [FromQuery] string userId)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();

            var postLike = _context.PostLikes.FirstOrDefault(pl => pl.PostId == id && pl.UserId == userId);
            if (postLike == null)
            {
                // Add like
                post.Likes += 1;
                _context.PostLikes.Add(new PostLike { PostId = id, UserId = userId });
            }
            else
            {
                // Remove like
                post.Likes -= 1;
                _context.PostLikes.Remove(postLike);
            }

            _context.Posts.Update(post);
            _context.SaveChanges();

            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
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
