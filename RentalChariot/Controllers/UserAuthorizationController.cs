using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.DTOs;
using RentalChariot.Models;
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
                return Unauthorized("Wrong login or password");
            //Check if User has LoginToken delete LoginToken
            var existingToken = await _context.LoginTokens.FirstOrDefaultAsync(t => t.UserId == user.UserId);
            if (existingToken != null)
                _context.LoginTokens.Remove(existingToken);

            var token = LoginToken.Create(user.UserId);

            user.InitializeUserState();
            user.Login();

            _context.LoginTokens.Add(token);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                loginToken = token.Token,
                message = "Login successful"
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");
            if(request.Password == null || request.Name == null)
                return BadRequest("Name or Password is null");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
            user.InitializeUserState();

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
            var token = await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == request.LoginToken);
            if (token == null)
                return NotFound("Token Not Found, mb you already logout");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == token.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.LogOut();
            _context.LoginTokens.Remove(token);
            await _context.SaveChangesAsync();

            return Ok($"{user.UserId}: Logout successful");
        }

        [HttpPost("ban")]
        public async Task<IActionResult> Ban([FromBody] BanRequest request)
        {
            var adminInput = request.Admin;
            var userToBanInput = request.UserToBan;

            var admintoken = await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == adminInput.LoginToken);

            if (admintoken == null)
                return BadRequest("Admin should log in");
            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == admintoken.UserId);

            if (adminUser == null)
                return NotFound("Admin not found");

            var Admin = adminUser as Admin;

            if (Admin == null)
                return Unauthorized("Permission denied (You are not Admin)");

            var User = await _context.Users.FirstOrDefaultAsync(u => u.Name == userToBanInput.Name);

            if (User == null)
                return NotFound("User not found");

            if (User.StateName == "Banned")
                return BadRequest("User Already Banned");

            Admin.Ban(User);

            await _context.SaveChangesAsync();
            return Ok("Ban successful");
        }


        [HttpPost("unban")]
        public async Task<IActionResult> UnBan([FromBody] UnBanRequest request)
        {
            var adminInput = request.Admin;
            var userToBanInput = request.UserToUnBan;

            var admintoken = await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == adminInput.LoginToken);

            if (admintoken == null)
                return BadRequest("Admin should log in");
            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == admintoken.UserId);

            if (adminUser == null)
                return NotFound("Admin not found");

            var Admin = adminUser as Admin;

            if (Admin == null)
                return Unauthorized("Permission denied (You are not Admin)");

            var User = await _context.Users.FirstOrDefaultAsync(u => u.Name == userToBanInput.Name);

            if (User == null)
                return NotFound("User not found");

            if (User.StateName != "Banned")
                return BadRequest("User Already UnBanned");

            Admin.UnBan(User);
            await _context.SaveChangesAsync();
            return Ok("UnBan successful");
        }

    }
}
