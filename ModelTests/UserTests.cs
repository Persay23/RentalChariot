using FluentAssertions;
using RentalChariot.Models;
using RentalChariot.UserManagement;

namespace ModelTests
{
    public class UserTests
    {
        [Fact]
        public void UserCanBeCreated()
        {
            var user = new User { Id = 1, Name = "TestName", Password = "TestPass" };

            user.Id.Should().Be(1);
            user.Name.Should().Be("TestName");
            user.Password.Should().Be("TestPass");
            user.StateName.Should().Be("UnActive");
        }
        [Fact]
        public void AdminCanBeCreated()
        {
            var user = new Admin { Name = "TestName", Password = "TestPass" };

            user.Name.Should().Be("TestName");
            user.Password.Should().Be("TestPass");
            user.StateName.Should().Be("UnActive");
        }

        [Fact]
        public void UserLoginShouldChangeStateName()
        {
            var user = new User { };

            user.Login();

            user.StateName.Should().Be("Active");
        }
        [Fact]
        public void AdminLoginShouldChangeStateName()
        {
            var user = new Admin { };

            user.Login();

            user.StateName.Should().Be("Active");
        }
        [Fact]
        public void UserLogOutShouldChangeStateName()
        {
            var user = new User { };

            user.Login();
            user.LogOut();

            user.StateName.Should().Be("UnActive");
        }
        [Fact]
        public void BanShouldChangeStateName()
        {
            var user = new User { };
            var admin = new Admin { };

            admin.Ban(user);
            user.StateName.Should().Be("Banned");
            user.Login();
            user.StateName.Should().Be("Banned");
            user.UserState.Should().BeOfType<BannedState>(); 

        }
        [Fact]
        public void UnBanShouldChangeStateName()
        {
            var user = new User { };
            var admin = new Admin { };

            admin.Ban(user);
            user.StateName.Should().Be("Banned");
            admin.UnBan(user);
            user.StateName.Should().Be("UnActive");

        }
    }
}