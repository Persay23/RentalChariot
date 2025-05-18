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
            user.Login();
            _context.SaveChanges();
            return Ok("Login successful");
        }
        //Not Cool everyone could use it
        //TODO maybe create some kinda class Token that only User will see 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] UserLogOutRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.LogOut();  
            await _context.SaveChangesAsync();

            return Ok("Logout successful");
        }
    }
}
