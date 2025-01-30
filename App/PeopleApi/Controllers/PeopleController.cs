using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Text.Json;

namespace PeopleApi.Controllers
{
    [ApiController]
        [Route("api/people")]
        public class PeopleController : ControllerBase
        {
            private readonly UserProfileRepository _repository;

            public PeopleController(UserProfileRepository repository)
            {
                _repository = repository;
            }

        [HttpPost]
        public IActionResult CreateProfile([FromBody] UserProfile profile)
        {
            if (string.IsNullOrEmpty(profile.UserId))
            {
                return BadRequest(new { Message = "UserId is required" });
            }

            _repository.AddProfile(profile);
            return CreatedAtAction(nameof(GetProfile), new { id = profile.UserId }, profile);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
                var profile = _repository.GetProfileByUserId(id.ToString());
                if (profile == null) return NotFound();

                return Ok(profile);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProfile(string id, [FromBody] UserProfile profile)
        {
            if (profile == null || id != profile.UserId)
            {
                return BadRequest(new { Message = "Invalid profile data." });
            }

            _repository.UpdateProfile(profile); // Aktualizacja w bazie danych
            return Ok(new { Message = "Profile updated successfully!" });
        }
    }
    }