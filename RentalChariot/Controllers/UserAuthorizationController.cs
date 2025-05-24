using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalChariot.Data;
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
        private readonly IUnitOfWork _unitOfWork;

        public UserAuthorizationController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
                return BadRequest("Invalid login request");
            Console.WriteLine(_unitOfWork is null);
            Console.WriteLine(_unitOfWork.Users is null);

            var user = await _unitOfWork.Users.GetByName(request.Name);

            if (user == null || user.Password != request.Password)
                return Unauthorized("Wrong login or password");

            var Token = await _unitOfWork.LoginTokens.GetTokenByUserId(user.Id);

            if (Token is not null)
                _unitOfWork.LoginTokens.Remove(Token);

            var token = LoginToken.Create(user.Id);

            user.InitializeUserState();
            user.Login();

            _unitOfWork.LoginTokens.Add(token);
            _unitOfWork.Complete();

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
            if (await _unitOfWork.Users.IsExist(request.Name))
                return Unauthorized("Account already exist");

            var newUser = new User
            {
                Name = request.Name,
                Password = request.Password
            };
            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Complete();

            return Ok("Account created successfully");
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] CurrentUser request)
        {
            var token = await _unitOfWork.LoginTokens.GetToken(request.LoginToken);
            if (token == null)
                return NotFound("Token Not Found, mb you already logout");
            var user = await _unitOfWork.Users.GetUserByToken(request.LoginToken);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.LogOut();

            _unitOfWork.LoginTokens.Remove(token);

            _unitOfWork.Complete();

            return Ok($"{user.Id}: Logout successful");
        }

        [HttpPost("ban")]
        public async Task<IActionResult> Ban([FromBody] BanRequest request)
        {
            var adminInput = request.Admin;
            var userToBanInput = request.UserToBan;

            var admintoken = await _unitOfWork.LoginTokens.GetToken(adminInput.LoginToken);
            if (admintoken == null)
                return BadRequest("Admin should log in");

            var adminUser = await _unitOfWork.Users.GetUserByToken(admintoken);
            if (adminUser == null)
                return NotFound("Admin not found");

            var Admin = adminUser as Admin;
            if (Admin == null)
                return Unauthorized("Permission denied (You are not Admin)");

            var User = await _unitOfWork.Users.GetByName(userToBanInput.Name);
            if (User == null)
                return NotFound("User not found");

            if (User.StateName == "Banned")
                return BadRequest("User Already Banned");

            Admin.Ban(User);

            _unitOfWork.Complete();
            return Ok("Ban successful");
        }


        [HttpPost("unban")]
        public async Task<IActionResult> UnBan([FromBody] UnBanRequest request)
        {
            var adminInput = request.Admin;
            var userToBanInput = request.UserToUnBan;

            var admintoken = await _unitOfWork.LoginTokens.GetToken(adminInput.LoginToken);
            if (admintoken == null)
                return BadRequest("Admin should log in");

            var adminUser = await _unitOfWork.Users.GetUserByToken(admintoken);
            if (adminUser == null)
                return NotFound("Admin not found");

            var Admin = adminUser as Admin;
            if (Admin == null)
                return Unauthorized("Permission denied (You are not Admin)");

            var User = await _unitOfWork.Users.GetByName(userToBanInput.Name);
            if (User == null)
                return NotFound("User not found");

            if (User.StateName != "Banned")
                return BadRequest("User Already UnBanned");

            Admin.UnBan(User);
            _unitOfWork.Complete();
            return Ok("UnBan successful");
        }

    }
}
