using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalChariot.Db;

namespace Database.Tests
{
    public class DataBaseTest
    {
        private readonly RentalChariotDbContext db;

        public DataBaseTest()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<DataBaseTest>()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var dbOptionsBuilder = new DbContextOptionsBuilder<RentalChariotDbContext>();
            dbOptionsBuilder.UseSqlServer(connectionString);

            db = new RentalChariotDbContext(dbOptionsBuilder.Options);
        }

        [Fact]
        public void DatabaseCanConnect_ShouldReturnTrue_Successfully()
        {
            var canConnect = db.Database.CanConnect();
            canConnect.Should().BeTrue();
        }
    }
}