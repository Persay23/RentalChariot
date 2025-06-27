using FluentAssertions;
using RentalChariot.Models;
using System.Runtime.ConstrainedExecution;

namespace ModelTests
{
    public class RentTests
    {
        private readonly User user;
        private readonly Car car;
        private readonly DateTime Today = DateTime.Today;
        

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

        [Fact]
        public void RentCanBeCreated()
        {
            user.Login();

            Rent rent = Rent.CreateRent(user, car, Today, Today.AddDays(1));
            rent.Id = 1;

            rent.Id.Should().Be(1);
            rent.UserId.Should().Be(1);
            rent.CarId.Should().Be(2);
            rent.ReturnDate.Should().Be(Today.AddDays(1));
            rent.RentDate.Should().Be(Today);
            rent.StateName.Should().Be("UnPaid");

        }

        [Fact]
        public void RentCanBePayed()
        {
            user.Login();

            Rent rent = Rent.CreateRent(user, car, Today, Today.AddDays(1));
            rent.Id = 1;
            rent.Pay();
            Console.WriteLine(user.StateName);

            rent.StateName.Should().Be("UnActive");

        }

        [Fact]
        public void RentCanBeStartAfterPaying()
        {
            user.Login();

            Rent rent = Rent.CreateRent(user, car, Today, Today.AddDays(1));
            rent.Id = 1;
            rent.Pay();

            rent.StartRent();

            rent.StateName.Should().Be("Active");

        }

        [Fact]
        public void RentCannotBeCreatedIfCarStateIsDeactivated()
        {
            user.Login();
            car.Deactivate();

            Rent? rent = Rent.CreateRent(user, car, Today, Today.AddDays(1));

            rent.Should().BeNull();

        }

        [Fact]
        public void RentCannotBeCreatedIfUserStateIsUnActive()
        {
            Rent? rent = Rent.CreateRent(user, car, Today, Today.AddDays(1));

            rent.Should().BeNull();
        }
    }
}