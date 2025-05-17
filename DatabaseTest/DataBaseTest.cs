using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RentalChariot.Db;

namespace Database.Tests
{
    public class DataBaseTest
    {
        [Fact]
        public void DatabaseCanConnect_ShouldReturnTrue_Successfully()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<RentalChariotDbContext>();
            dbOptionsBuilder.UseSqlServer("Server=tcp:databasevoks.database.windows.net,1433;Initial Catalog=RentalChariot;User ID=admin1;Password=Password1;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            var db = new RentalChariotDbContext(dbOptionsBuilder.Options);

            var canConnect = db.Database.CanConnect();
            
            canConnect.Should().BeTrue();
        }
    }
}