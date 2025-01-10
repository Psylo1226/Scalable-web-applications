using Microsoft.AspNetCore.Mvc;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("People API is running");
        }
    }
}