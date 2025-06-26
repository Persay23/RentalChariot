using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentalChariot.RentManagement;

namespace RentalChariot.Models
{
    public class Rent
    {
        [Timestamp]
        public byte[] Version { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [NotMapped]
        public IRentState State { get; set; }

        [Required]
        public string StateName { get; set; }

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

            var userid = user.Id;
            var carid = car.Id;

            return new Rent(userid, carid, RentDate, ReturnDate);
        }

        public void UpdateState(Func<IRentState, IRentState> func)
        {
            State = func(State);
            StateName = State.Name;
        }

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
    }
}