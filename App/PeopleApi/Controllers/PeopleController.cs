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
                _repository.AddProfile(profile);
                return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
            }

            [HttpGet("{id}")]
            public IActionResult GetProfile(int id)
            {
                var profile = _repository.GetProfileByUserId(id.ToString());
                if (profile == null) return NotFound();

                return Ok(profile);
            }
        }
    }