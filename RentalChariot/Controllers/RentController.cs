using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.Models;
using RentalChariot.Models.RentModel.Services;

namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly RentService _rentService;

        public RentController(RentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RentRequest request)
        {

            if (request == null)
                return BadRequest("Request is null");

            var requestCarId = request.CarId;
            var userToken = request.User.LoginToken;

            var rent = await _rentService.CreateRentAsync(requestCarId, userToken, DateTime.Now.AddSeconds(5), DateTime.Now.AddSeconds(15));


            return Ok();
        }
    }
}
