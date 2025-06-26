using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Data;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.Models;

namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CarController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");
            if (await _unitOfWork.Cars.IsExist(request.Number) )
                return Unauthorized("Car already exist with this number");

            var newCar = Car.CreateCar(
                request.Brand,
                request.Model,
                request.Number,
                request.ProdYear,
                request.Color,
                request.EngineVol,
                request.Mileage
                );
            _unitOfWork.Cars.Add(newCar);
            _unitOfWork.Complete();
            return Ok("Car created");
        }
    }
}