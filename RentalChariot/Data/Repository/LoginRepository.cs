using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.Models;
using RentalChariot.Repository;

namespace RentalChariot.LoginModel
{
    public class LoginRepository : Repository<LoginToken>, ILoginRepository
    {
        public LoginRepository(RentalChariotDbContext context) : base(context)
        {
        }

        public async Task<LoginToken> GetToken(string token)
        {
            return await _context.LoginTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<LoginToken> GetTokenByUserId(int userId)
        {
            return await _context.LoginTokens.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<bool> IsExistForThisUserId(int userId)
        {
            return await _context.LoginTokens.AnyAsync(t => t.UserId == userId);
        }
    }
}