using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using RentalChariot.RentManagement;


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
        public DateTime RentDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [NotMapped]
        public IRentState State { get; set; }

        [Required]
        public string StateName { get; set; }


        //public decimal? Deposit {  get; set; } //To do add maxvalue 999999.99 minvalue 0

        //public decimal? UnitPrice { get; set; } //To do add maxvalue 999999.99 minvalue 0

        protected Rent(int userId, int carId, DateTime rentDate, DateTime returnDate) {
            UserId = userId;
            CarId = carId;
            RentDate = rentDate;
            ReturnDate = returnDate;
            this.State = new UnPaidState();
            StateName = State.Name;

        }
        public static Rent CreateRent(User user, Car car, DateTime rentDay, DateTime returnDay)
        {
            if (!user.IsAbleToCreateRent())
                return null;
            if (!car.IsAvaliable())
                return null;

            var RentDate = rentDay;
            var ReturnDate = returnDay;

            var userid = user.UserId;
            var carid = car.CarId;

            return new Rent(userid, carid, RentDate, ReturnDate);

        }

        public void UpdateState(Func<IRentState, IRentState> func)
        {
            State = func(State);
            StateName = State.Name;
        }

        //private async Task WaitUntilStartDate()
        //{
        //    while (!IsStart()) {
        //        await Task.Delay(50);
        //    }

        //    if (State.isPaid == false)
        //    {
        //        UpdateState(State => State.Cancel());
        //        return;
        //    }

        //    UpdateState(State => State.StartRent());
        //    Timer = () => WaitUntilReturnDate();
        //    _ = Start();

        //}

        //private async Task WaitUntilReturnDate()
        //{
        //    while (!IsEnd())
        //    {
        //        await Task.Delay(50);
        //    }

        //    UpdateState(State => State.EndRent());
        //}

        public bool IsStart()
        {
            return RentDate <= DateTime.Now;
        }

        public bool IsEnd()
        {
            return ReturnDate <= DateTime.Now;
        }

        public void Cancel()
        {
            UpdateState(State => State.Cancel());
        }

        public void Pay()
        {
            UpdateState(State => State.PayForRent());
        }

        public void StartRent() => UpdateState(s => s.StartRent());

        public void EndRent() => UpdateState(s => s.EndRent());
        //public async Task Start()
        //{
        //    await Timer();
        //}

    }
}
