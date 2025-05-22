using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalChariot.Db;
using RentalChariot.Models.RentModel.Services;
using RentalChariot.UserManagement;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<RentService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                                                                                      
builder.Services.AddDbContext<RentalChariotDbContext>(options => 
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthorization();
app.MapControllers();
app.Run();
