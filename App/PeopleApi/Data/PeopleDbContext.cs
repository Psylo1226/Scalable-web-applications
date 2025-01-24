using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;

namespace PeopleApi.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}