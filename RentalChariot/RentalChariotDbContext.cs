using Microsoft.EntityFrameworkCore;
using RentalChariot.UserManagement;

namespace RentalChariot.Db;

public class RentalChariotDbContext : DbContext
{
    public RentalChariotDbContext(DbContextOptions<RentalChariotDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
