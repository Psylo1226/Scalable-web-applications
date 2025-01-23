using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Security.Claims;

namespace PeopleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class People : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public People(PeopleDbContext context)
        {
            _context = context;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto profileDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    FirstName = profileDto.FirstName,
                    LastName = profileDto.LastName,
                    Description = profileDto.Description,
                    AvatarUrl = profileDto.AvatarUrl
                };
                _context.UserProfiles.Add(profile);
            }
            else
            {
                profile.FirstName = profileDto.FirstName;
                profile.LastName = profileDto.LastName;
                profile.Description = profileDto.Description;
                profile.AvatarUrl = profileDto.AvatarUrl;
            }

            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }
    }
    public class UserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
    }
}