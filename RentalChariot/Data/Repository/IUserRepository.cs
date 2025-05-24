using RentalChariot.Repository;

namespace RentalChariot.Models.UserModel.Services
{
    public interface IUserRepository : IRepository<User>
    {
        //No Ideas what can i write here, but propably i will do somethink here
        //Task<User> GetUserByTokenAsync(string token); //Stupid Idea

        Task<User> GetByName(string name);

        Task<bool> IsExist(string name);

        Task<int> GetUserIdByToken(string token);

        Task<User> GetUserByToken(string token);

        Task<User> GetUserByToken(LoginToken token);
    }
}
