using Microsoft.EntityFrameworkCore;
using RentalChariot.Models;
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
    public DbSet<LoginToken> LoginTokens { get; set; }
    public DbSet<Car> Cars {  get; set; }
    public DbSet<Rent> Rents {  get; set; }      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
