using RentalChariot.Data.Repository;
using RentalChariot.LoginModel;
using RentalChariot.Models.UserModel.Services;

namespace RentalChariot.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ILoginRepository LoginTokens { get; }
        ICarRepository Cars { get; }
        IRentRepository Rents { get; }

        int Complete();
        Task<int> CompleteAsync();


    }
}
