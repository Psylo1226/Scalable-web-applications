using IdentityApi.Models;

namespace IdentityApi.Data
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public List<User> SearchUsers(string query)
        {
            return _context.Users
                .Where(u => u.Username.Contains(query))
                .ToList();
        }
    }
}
