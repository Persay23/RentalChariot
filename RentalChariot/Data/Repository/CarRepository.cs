using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using RentalChariot.Models;
using RentalChariot.Repository;

namespace RentalChariot.Data.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(RentalChariotDbContext context) : base(context) 
        {
            // TODO Initialize the repository with the context
        }

        public async Task<bool> IsAvaliableToRent(int Carid)
        {
            var Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == Carid);
            return Car.StateName == "Avaliable";
        }

        public async Task<bool> IsExist(string Number)
        {
            return await _context.Cars.AnyAsync(c => c.Number == Number);
        }
    }
}