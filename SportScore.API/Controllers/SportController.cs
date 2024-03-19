using Microsoft.AspNetCore.Mvc;

namespace SportScore.API.Controllers
{
    public class SportController : ControllerBase
    {
        [HttpGet("api/sport/test")]
        public IActionResult TestMessage(Guid guid)
        {
            return Ok("Test servcie works");
        }
    }
}