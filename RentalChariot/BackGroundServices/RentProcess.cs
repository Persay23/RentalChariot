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

            rent.UpdateState(state => state.StartRent());

            Console.WriteLine(rent.State.isPaid);

            if (!rent.State.isPaid) {
                rent.Cancel();
            }
            _unitOfWork.Complete();
        }

        private async Task WaitUntilEndDateAsync()
        {
            while (!rent.IsEnd())
            {
                await Task.Delay(50);
            }
        }
       
        private void EndRent()
        {
            _unitOfWork.Rents.Update(rent);
            rent = _unitOfWork.Rents.Get(rent.Id);
            rent.State = RentStateFactory.Update(rent.StateName);
            rent.UpdateState(state => state.EndRent());
            _unitOfWork.Complete();
        }

        public async Task PayAsync(int id)
        {
            rent = _unitOfWork.Rents.Get(rent.Id);
            rent.State = RentStateFactory.Update(rent.StateName);
            rent.UpdateState(state => state.PayForRent());
            _unitOfWork.Complete();
        }
    }
}