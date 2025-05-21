using RentalChariot.CarManagement;
using System.ComponentModel.DataAnnotations;

namespace RentalChariot.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(16)]
        public string Model { get; set; }

        [Required]
        [StringLength(8)]
        public string Number { get; set; }

        [Required]
        public DateTime ProdYear { get; set; }

        [Required]
        [StringLength(16)]
        public string Color { get; set; }

        [Required]
        public short EngineVol { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string StateName { get; set; }

        private ICarState State { get; set; }   


        private Car(int carId, string brand, string model, string number, DateTime prodyear, string color, short enginevol, int miliage, ICarState State)
        {
            CarId = carId;
            Brand = brand;
            Model = model;
            Number = number;
            ProdYear = prodyear;
            Color = color;
            EngineVol = enginevol;
            Mileage = miliage;

            StateName = State.StateName;
        }

        public static Car CreateCar(int carId, string brand, string model, string number, DateTime prodyear, string color, short enginevol, int miliage)
        {
            return new Car(carId, brand, model, number, prodyear, color, enginevol, miliage, new AvaliableState());
        }

        public void Activate()
        {
            UpdateState(State => State.Activate());
        }
        public void Deactivate()
        {
            UpdateState(State => State.Deactivate());
        }
        public void SendToRent()
        {
            UpdateState(State => State.SendToRent());
        }
        public void SendFromRent()
        {
            UpdateState(State => State.SendFromRent());
        }

        public void UpdateState(Func<ICarState, ICarState> func)
        {
            State = func(State);
            StateName = State.StateName;
        }

        public bool IsAvaliable()
        {
            return State.IsAvaliable;
        }
    }

}
