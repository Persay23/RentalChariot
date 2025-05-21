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
                carId: 3,
                brand: "Toyota",
                model: "Corolla",
                number: "ABC-1234",
                prodyear: new DateTime(2020, 1, 1),
                color: "White",
                enginevol: 1600,
                miliage: 50000
            );
            car.CarId.Should().Be( 3 );
            car.Brand.Should().Be("Toyota");

        }
    }
}