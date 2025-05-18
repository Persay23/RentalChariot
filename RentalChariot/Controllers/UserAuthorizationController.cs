using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.DTOs;

namespace RentalChariot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthorizationController : ControllerBase
    {
        private readonly RentalChariotDbContext _context;

        public UserAuthorizationController(RentalChariotDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Wrong login or password");
            }
            return Ok("Login successful");
        }
    }
}
