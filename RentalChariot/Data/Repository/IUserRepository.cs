using RentalChariot.Repository;

namespace RentalChariot.Models.UserModel.Services
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string name);

        Task<bool> IsExist(string name);

        Task<int> GetUserIdByToken(string token);

        Task<User> GetUserByToken(string token);

        Task<User> GetUserByToken(LoginToken token);
    }
}