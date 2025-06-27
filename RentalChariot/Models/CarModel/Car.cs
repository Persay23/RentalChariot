using RentalChariot.CarManagement;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RentalChariot.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        
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

        protected Car(string brand, string model, string number, DateTime prodYear, string color, short engineVol, int mileage)
        {
            Brand = brand;
            Model = model;
            Number = number;
            ProdYear = prodYear;
            Color = color;
            EngineVol = engineVol;
            Mileage = mileage;
            State = new AvaliableState();
            StateName = State.StateName;
        }

        public static Car CreateCar( string brand, string model, string number, DateTime prodYear, string color, short engineVol, int mileage)
        {
            return new Car(brand, model, number, prodYear, color, engineVol, mileage);
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

        public void InitializeCarState()
        {
            State = StateName switch
            {
                "UnAvaliable" => new UnAvailableState(),
                "Avaliable" => new AvaliableState(),
                "Deactivated" => new DeactivatedState(),
            };
        }
    }
}