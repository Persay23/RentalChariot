using RentalChariot.Models;
using RentalChariot.Repository;

namespace RentalChariot.Data.Repository
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<bool> IsAvaliableToRent(int Carid);

        Task<bool> IsExist(string Number);
    }
}
