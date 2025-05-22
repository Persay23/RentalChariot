using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.Models;

namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly RentalChariotDbContext _context;

        public CarController(RentalChariotDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");
            var car = await _context.Cars.SingleOrDefaultAsync(c => c.Number == request.Number);
            if (car != null)
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
            _context.Cars.Add(newCar);
            _context.SaveChanges();
            return Ok("Car created");

        }

    }
}
