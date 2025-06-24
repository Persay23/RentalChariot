using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentalChariot.BackGroundServices;
using RentalChariot.Data;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.Models;


namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RentController(IUnitOfWork unitofWork, IServiceScopeFactory serviceScopeFactory)
        {
            _unitOfWork = unitofWork;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RentRequest request)
        {

            if (request == null)
                return BadRequest("Request is null");

            var requestCarId = request.CarId;
            var userToken = request.User.LoginToken;

            var user = await _unitOfWork.Users.GetUserByToken(userToken);
            var Car = _unitOfWork.Cars.Get(requestCarId);


            var rent = _unitOfWork.Rents.CreateRent(user, Car, DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(20));
            
            if (rent == null)
                return BadRequest("Failed to create rent");

            _ = ProcessRentInBackground(rent.Id);

            return Ok();
        }

        [HttpPost("Pay/{rentId}")]
        public async Task<IActionResult> Pay(int rentId)
        {
            var rent = _unitOfWork.Rents.Get(rentId);

            if (rent == null)
                return NotFound("Rent not found");

            var rentProcess = new RentProcess(_unitOfWork, ref rent);
            await rentProcess.PayAsync(rentId);

            return Ok("Rent paid successfully");
        }


        private async Task ProcessRentInBackground(int rentId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var rent = unitOfWork.Rents.Get(rentId); 

                if (rent == null) return;

                var rentProcess = new RentProcess(unitOfWork, ref rent);
                await rentProcess.ProcessRentAsync();
            }
        }
    }
}
