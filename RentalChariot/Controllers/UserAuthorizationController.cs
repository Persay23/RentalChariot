using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.UserManagement;

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
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");
            if(request.Password == null || request.Name == null)
                return BadRequest("Name or Password is null");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
            if (user != null)
                return Unauthorized("Account already exist");
            var newUser = new User
            {
                Name = request.Name,
                Password = request.Password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("Account created successfully");
        }
        //Not Cool everyone could use it
        //TODO maybe create some kinda class Token that only User will see 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] CurrentUser request)
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

        [HttpPost("ban")]
        public async Task<IActionResult> Ban([FromBody] BanRequest request)
        {
            var adminInput = request.Admin;
            var userToBanInput = request.UserToBan;

            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == adminInput.Name); 
            if (adminUser == null)
            {
                return NotFound("Admin not found");
            }
            var Admin = adminUser as Admin;
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Name == userToBanInput.Name);
            if (User == null)
            {
                return NotFound("Admin not found");
            }
            Admin.Ban(User);
            await _context.SaveChangesAsync();
            return Ok("Ban successful");
        }


        [HttpPost("unban")]
        public async Task<IActionResult> UnBan([FromBody] UnBanRequest request)
        {
            var adminInput = request.Admin;
            var userToUnBanInput = request.UserToUnBan;

            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == adminInput.Name);
            if (adminUser == null)
            {
                return NotFound("Admin not found");
            }
            var Admin = adminUser as Admin;
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Name == userToUnBanInput.Name);
            if (User == null)
            {
                return NotFound("Admin not found");
            }
            Admin.UnBan(User);
            await _context.SaveChangesAsync();
            return Ok("UnBan successful");
        }

    }
}
