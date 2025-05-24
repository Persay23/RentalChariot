
using Microsoft.EntityFrameworkCore;
using RentalChariot.Data;
using RentalChariot.Models;
using RentalChariot.Models.RentModel;

namespace RentalChariot.BackGroundServices
{
    public class RentProcess
    {
        private readonly IUnitOfWork _unitOfWork;
        public Rent rent;

        public RentProcess(IUnitOfWork unitOfWork, ref Rent rent)
        {

            _unitOfWork = unitOfWork;
            this.rent = rent;
        }

        public async Task ProcessRentAsync()
        {

            await WaitUntilStartDateAsync();
            StartRent();
            await WaitUntilEndDateAsync();
            EndRent();
        }

        private async Task WaitUntilStartDateAsync()
        {
            //Console.WriteLine("One");
            while (!rent.IsStart())
            {
                await Task.Delay(50);
            }
        }

        private void StartRent()
        {
            _unitOfWork.Rents.Update(rent);
            rent = _unitOfWork.Rents.Get(rent.Id);
            rent.State = RentStateFactory.Update(rent.StateName);
            //Console.WriteLine("StartRent");
            //Console.WriteLine($"{rent.StateName}      Start1");
            //Console.WriteLine($"{rent.State}      Start2");
            rent.UpdateState(state => state.StartRent());
            //Console.WriteLine($"{rent.StateName}      Start3");
            //Console.WriteLine($"{rent.State}      Start4");
            _unitOfWork.Complete();
        }

        private async Task WaitUntilEndDateAsync()
        {
            //Console.WriteLine("WaitUntilEndDateAsync");
            while (!rent.IsEnd())
            {
                await Task.Delay(50);
            }
        }
        

        private void EndRent()
        {
            //Console.WriteLine("EndRent");
            _unitOfWork.Rents.Update(rent);
            rent = _unitOfWork.Rents.Get(rent.Id);
            //Console.WriteLine($"{rent.StateName}      End1");
            //Console.WriteLine($"{rent.State}      End2");
            rent.State = RentStateFactory.Update(rent.StateName);
            rent.UpdateState(state => state.EndRent());
            //Console.WriteLine($"{rent.StateName}      End3");
            //Console.WriteLine($"{rent.State}      End4");
            _unitOfWork.Complete();
        }

        public async Task PayAsync(int id)
        {
            rent = _unitOfWork.Rents.Get(rent.Id);
            //Console.WriteLine($"{rent.StateName}      Pay1");
            //Console.WriteLine($"{rent.State}      Pay2");
            rent.State = RentStateFactory.Update(rent.StateName);
            //Console.WriteLine($"{rent.StateName}      Pay3");
            //Console.WriteLine($"{rent.State}      Pay4");
            rent.UpdateState(state => state.PayForRent());
            //Console.WriteLine($"{rent.StateName}      Pay5");
            //Console.WriteLine($"{rent.State}      Pay6");
            _unitOfWork.Complete();
        }
    }
}