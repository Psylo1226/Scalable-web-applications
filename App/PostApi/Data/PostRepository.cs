using PostApi.Models;

namespace PostApi.Data
{
    public class PostRepository
    {
        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public Post? GetPostById(int id)
        {
            return _context.Posts.Find(id);
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void RemovePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
