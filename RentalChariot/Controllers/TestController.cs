using Microsoft.AspNetCore.Mvc;

namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test successful!");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"You requested item with ID = {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok($"Received: {value}");
        }
    }
}