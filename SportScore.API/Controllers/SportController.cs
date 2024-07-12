using Microsoft.AspNetCore.Mvc;
using SportScore.API.Dtos;
using SportScore.API.Services;

namespace SportScore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ISportService _service;

        public SportController(ISportService service)
        {
            _service = service;
        }

        [HttpGet("test")]
        public IActionResult TestMessage(Guid guid)
        {
            return Ok("Test service works");
        }

        /// <summary>
        /// Calculates and provides the winner of Best of 3 match with 15 points to win set
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewSportDto input)
        {
            var result = await _service.CalculateAndAddAsync(input);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchInfo(Guid id)
        {
            var result = await _service.GetMatchInfoByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }
    }
}