using RentalChariot.Models;

namespace ModelTests
{
    public class RentTests
    {
        private readonly User user;
        private readonly Car car;

        public RentTests() {
            user = new User { Id = 1, Name = "TestName", Password = "TestPass" };
          
            car = Car.CreateCar
            (
                brand: "Toyota",
                model: "Corolla",
                number: "ABC-1234",
                prodYear: new DateTime(2020, 1, 1),
                color: "White",
                engineVol: 1600,
                mileage: 50000
            );
            car.Id = 2;
        }
    }
}