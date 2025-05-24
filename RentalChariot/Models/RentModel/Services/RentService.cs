using Microsoft.EntityFrameworkCore;
using RentalChariot.BackGroundServices;
using RentalChariot.Data;
using RentalChariot.Db;
using System.Threading;
using System.Threading.Tasks;
//TODELEATE
//namespace RentalChariot.Models.RentModel.Services
//{
//    public class RentService
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public RentService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<Rent> CreateRentAsync(int requestCarId, string userToken, DateTime Start, DateTime End)
//        {
//            var car = _unitOfWork.Cars.Get(requestCarId);
//            if (car == null) return null;
//            car.InitializeCarState();

   
//            var UserToken = await _unitOfWork.LoginTokens.GetToken(userToken);
//            if (UserToken == null) return null;

            
//            var user = _unitOfWork.Users.Get(UserToken.UserId);
//            if (user == null) return null;
//            user.InitializeUserState();

//            var rent = Rent.CreateRent(user, car, Start, End);
//            if (rent == null) return null;

//            rent.Pay();
//            _unitOfWork.Rents.Add(rent);
//            _unitOfWork.Complete();

//            var rentProcess = new RentProcess(_unitOfWork);
//            _ = Task.Run(() => rentProcess.ProcessRentAsync(rent));

//            return rent;
//        }

        //    public async Task<Rent> ProcessRentAsync(Rent rent)
        //    {
        //        await WaitUntilStartDateAsync(rent);
        //        await WaitUntilEndDateAsync(rent);
        //        return rent;
        //    }

        //    private async Task WaitUntilStartDateAsync(Rent rent)
        //    {
        //        while (!rent.IsStart())
        //        {
        //            await Task.Delay(50);
        //        }

        //        if (rent.State.isPaid == false)
        //        {
        //           rent.UpdateState(State => State.Cancel());
        //           await SaveRentAsync(rent);
        //           return;
        //        }

        //        rent.UpdateState(State => State.StartRent());
        //        await SaveRentAsync(rent);
        //    }
        //    private async Task WaitUntilEndDateAsync(Rent rent)
        //    {
        //        while (!rent.IsEnd())
        //        {
        //            await Task.Delay(50);
        //        }

        //        rent.UpdateState(State => State.EndRent());
        //        await SaveRentAsync(rent);
        //    }

        //public async Task SaveRentAsync(Rent rent)
        //{
        //    rent.StateName = rent.State.Name;

        //    await _context.SaveChangesAsync();
        //}
//    }
//}
