using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;
using System.Threading.Tasks;

namespace RentalChariot.Models.RentModel.Services
{
    public class RentService
    {
        private readonly RentalChariotDbContext _context;

        public RentService(RentalChariotDbContext context)
        {
            _context = context;
        }   

        public async Task StartRentProccess(Rent rent)
        {
            var rentInDb = await _context.Rents.FindAsync(rent.RentId);
            if (rentInDb == null)
                return;
            
        }

        public async Task<Rent> CreateRentAsync(int requestCarId, string userToken, DateTime Start, DateTime End)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(c => c.CarId == requestCarId);
            if(car == null) return null;
            car.InitializeCarState();
            var UserToken = await _context.LoginTokens.SingleOrDefaultAsync(t => t.Token == userToken);
            if (UserToken == null) return null;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == UserToken.UserId);
            if (user == null) return null;
            user.InitializeUserState();
            var rent = Rent.CreateRent(user, car, Start, End);
            if(rent == null) return null;
            _context.Rents.Add(rent);
            await SaveRentAsync(rent);
            rent.Pay();
            await SaveRentAsync(rent);
            await WaitUntilStartDateAsync(rent);
            await WaitUntilEndDateAsync(rent);
            return rent;

        }

        private async Task WaitUntilStartDateAsync(Rent rent)
        {
            while (!rent.IsStart())
            {
                await Task.Delay(50);
            }

            if (rent.State.isPaid == false)
            {
               rent.UpdateState(State => State.Cancel());
               await SaveRentAsync(rent);
               return;
            }

            rent.UpdateState(State => State.StartRent());
            await SaveRentAsync(rent);
        }
        private async Task WaitUntilEndDateAsync(Rent rent)
        {
            while (!rent.IsEnd())
            {
                await Task.Delay(50);
            }

            rent.UpdateState(State => State.EndRent());
            await SaveRentAsync(rent);
        }

        public async Task SaveRentAsync(Rent rent)
        {
            rent.StateName = rent.State.Name;

            await _context.SaveChangesAsync(); 
        }
    }
}
