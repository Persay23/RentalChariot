using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using RentalChariot.Models;
using RentalChariot.RentManagement;
using RentalChariot.UserManagement;

namespace RentalChariot.Models
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }
        //I dont think we need RentEmployeeID and ReturnEmployeeID

        //Later add RentPlaceId 
        //[Required]
        //public int RentPlaceId { get; set; }

        //public int? ReturnPlaceId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public IRentState State { get; set; }

        public string StateName { get; set; }

        public Func<Task> Timer { get; set; }

        //Later Add RentStateName 

        //public decimal? Deposit {  get; set; } //To do add maxvalue 999999.99 minvalue 0

        //public decimal? UnitPrice { get; set; } //To do add maxvalue 999999.99 minvalue 0

        private Rent(int userId, int carId, DateTime rentDate, DateTime returnDate, IRentState State) {
            UserId = userId;
            CarId = carId;
            StartDate = rentDate;
            ReturnDate = returnDate;
            this.State = State;
            StateName = State.StateName;
            Timer = () => WaitUntilStartDate();
            _ = Start();
        }

        //TEST Method Later create a Method For All Posible Dates
        public static Rent CreateFiveSecondsRent(User user, Car car)
        {
            if (!user.UserState.IsAbleToCreateRent)
                return null;
            //CarState is not connected to Car
            //if(!car.CarState.IsAvaliable)
            //    return null;

            //Mb create some kinda check so only 1 user can create Rent, Mb it's better to write it in Controllers
            var RentDate = DateTime.Now.AddSeconds(1);
            //ForTestRentOnlyFor2Minutes
            var ReturnDate = DateTime.Now.AddSeconds(5);
            

            var State = new UnPaidState();

            var userid = user.UserId;

            var carid = car.CarId;

            return new Rent(userid, carid, RentDate, ReturnDate, State);
        }

        public static Rent CreateRent(User user, Car car, DateTime rentDay, DateTime returnDay)
        {
            if (!user.IsAbleToCreateRent())
                return null;
            if (!car.IsAvaliable())
                 return null;

            //Mb create some kinda check so only 1 user can create Rent, Mb it's better to write it in Controllers
            var RentDate = rentDay;
            //ForTestRentOnlyFor2Minutes
            var ReturnDate = returnDay;

            var State = new UnPaidState();

            var userid = user.UserId;

            var carid = car.CarId;

            return new Rent(userid, carid, RentDate, ReturnDate, State);
        }

        public void UpdateState(Func<IRentState, IRentState> func)
        {
            State = func(State);
            StateName = State.StateName;
        }
        public bool IsStart()
        {
            return StartDate <= DateTime.Now;
        }

        public bool IsEnd()
        {
            return ReturnDate <= DateTime.Now;
        }

        public void Cancel()
        {
            UpdateState(State => State.Cancel());
        }

        private async Task WaitUntilStartDate()
        {
            while (!IsStart()) {
                await Task.Delay(50);
            }

            if (State.isPaid == false)
            {
                UpdateState(State => State.Cancel());
                return;
            }

            UpdateState(State => State.StartRent());
            Timer = () => WaitUntilReturnDate();
            _ = Start();

        }

        private async Task WaitUntilReturnDate()
        {
            while (!IsEnd())
            {
                await Task.Delay(50);
            }

            UpdateState(State => State.EndRent());
        }

        //public void ChangeState(IRentState newState)
        //{
        //    State = newState;
        //}

        public void Pay()
        {
            UpdateState(State => State.PayForRent());
        }

        public async Task Start()
        {
            await Timer();
        }
    }
}
