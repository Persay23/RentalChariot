using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalChariot.Data;
using RentalChariot.Db;
using RentalChariot.UserManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<RentalChariotDbContext>(options => 
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapOpenApi();

app.UseAuthorization();
app.MapControllers();
app.Run();