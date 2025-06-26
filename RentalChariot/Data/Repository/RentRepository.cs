using Microsoft.EntityFrameworkCore;
using RentalChariot.BackGroundServices;
using RentalChariot.Data.Repository;
using RentalChariot.Db;
using RentalChariot.Models;
using RentalChariot.Repository;

namespace RentalChariot.LoginModel
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        public RentRepository(RentalChariotDbContext context) : base(context)
        {
            _context = context;
        }

        public Rent CreateRent(User user, Car car, DateTime Now, DateTime End)
        {
            if (car == null) return null;
            car.InitializeCarState();

            if (user == null) return null;
            user.InitializeUserState();

            var rent = Rent.CreateRent(user, car, Now, End);
            if (rent == null) return null;

            _context.Rents.Add(rent);
            _context.SaveChanges();

            return rent;
        }

        public Rent GetById(int id)
        {
            return null;
        }


        public Rent GetFresh(int id)
        {
            return null;
        }

        public async Task SaveRentAsync(Rent rent)
        {
            return;
        }

        public void Update(Rent rent)
        {
            _context.Entry(rent).Reload();
        }
    }
}