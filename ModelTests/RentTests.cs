using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RentalChariot.Models;
using RentalChariot.RentManagement;
using RentalChariot.UserManagement;

namespace ModelTests
{
    public class RentTests
    {
        private readonly User user;
        private readonly Car car;

        public RentTests() {
            user = new User { UserId = 1, Name = "TestName", Password = "TestPass" };
          
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
            car.CarId = 2;

        }
        //[Fact]
        //public void RentCanBeCreated()
        //{
        //    user.Login();
        //    var myRent = Rent.CreateFiveSecondsRent(user, car);

        //    myRent.UserId.Should().Be(1);
        //    myRent.CarId.Should().Be(2);
        //}
        //[Fact]
        //public void PayShouldChangeStateToUnactive()
        //{
        //    user.Login();
        //    var myRent = Rent.CreateFiveSecondsRent(user, car);
        //    myRent.Pay();

        //    myRent.State.Should().BeOfType<RentalChariot.RentManagement.UnActiveState>();
        //}
        //[Fact]
        //public async Task IfNotPayShouldChangeStateToCancelled()
        //{

        //    user.Login();
        //    var myRent = Rent.CreateFiveSecondsRent(user, car);

        //    await Task.Delay(6000);

        //    myRent.State.Should().BeOfType<RentalChariot.RentManagement.DeletedState>();
        //}
        //[Fact]
        //public async Task AfterSixSecondsRentShouldChangeState()
        //{

        //    user.Login();
        //    var myRent = Rent.CreateFiveSecondsRent(user, car);
        //    myRent.Pay();
        //    myRent.State.Should().BeOfType<RentalChariot.RentManagement.UnActiveState>();

        //    await Task.Delay(1000);

        //    myRent.IsStart().Should().BeTrue();
        //    myRent.IsEnd().Should().BeFalse();
        //    await Task.Delay(6000);

        //    myRent.IsEnd().Should().BeTrue();

        //    myRent.State.Should().BeOfType<EndedState>();
        //}
    }
}
