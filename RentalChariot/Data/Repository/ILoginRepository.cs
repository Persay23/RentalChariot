using RentalChariot.Repository;
using RentalChariot.Models;

namespace RentalChariot.LoginModel
{
    public interface ILoginRepository : IRepository <LoginToken>
    {

        Task<LoginToken> GetToken(string token);

        Task<bool> IsExistForThisUserId(int userId);

        Task<LoginToken> GetTokenByUserId(int userId); 


    }
}
