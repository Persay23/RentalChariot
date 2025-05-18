using FluentAssertions;
using RentalChariot.UserManagement;

namespace ModelTests
{
    public class UserTests
    {
        [Fact]
        public void UserCanBeCreated()
        {
            var user = new User { Name = "TestName", Password = "TestPass" };

            user.Name.Should().Be("TestName");
            user.Password.Should().Be("TestPass");
            user.StateName.Should().Be("UnActive");
            user.RoleName.Should().Be("User");
        }
        [Fact]
        public void AdminCanBeCreated()
        {
            var user = new Admin { Name = "TestName", Password = "TestPass" };

            user.Name.Should().Be("TestName");
            user.Password.Should().Be("TestPass");
            user.StateName.Should().Be("UnActive");
            user.RoleName.Should().Be("Admin");
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