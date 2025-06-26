using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.Repository;

namespace RentalChariot.Models.UserModel.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RentalChariotDbContext context) : base(context)
        {
        }

        public async Task<User> GetByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name); ; 
        }

        public async Task<bool> IsExist(string name)
        {
            return await _context.Users.AnyAsync(u => u.Name == name);
        }

        public async Task<int> GetUserIdByToken(string token)
        {
            var Token = await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == token);
            return Token.UserId;
        }

        public async Task<User> GetUserByToken(string token)
        {
            var Token = await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == token);
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == Token.UserId);
        }
        public async Task<User> GetUserByToken(LoginToken token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == token.UserId);
        }
    }
}