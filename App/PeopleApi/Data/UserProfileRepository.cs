using PeopleApi.Models;

namespace PeopleApi.Data
{
    public class UserProfileRepository
    {
        private readonly PeopleDbContext _context;

        public UserProfileRepository(PeopleDbContext context)
        {
            _context = context;
        }

        public UserProfile? GetProfileByUserId(string userId)
        {
            return _context.UserProfiles.FirstOrDefault(p => p.UserId == userId);
        }

        public void AddProfile(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            _context.SaveChanges();
        }

        public void UpdateProfile(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            _context.SaveChanges();
        }
    }
}
