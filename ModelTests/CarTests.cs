using FluentAssertions;
using RentalChariot.Models;

namespace ModelTests
{
    public class CarTests
    {
        [Fact]
        public void CarCanBeCreated()
        {
            var car = Car.CreateCar
            (
                brand: "Toyota",
                model: "Corolla",
                number: "ABC-1234",
                prodYear: new DateTime(2020, 1, 1),
                color: "White",
                engineVol: 1600,
                mileage: 50000
            );
            car.CarId = 3;
            car.CarId.Should().Be( 3 );
            car.Brand.Should().Be("Toyota");

        }
    }
}