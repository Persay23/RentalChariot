using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalChariot.Db;
using RentalChariot.UserManagement;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                                                                                      
builder.Services.AddDbContext<RentalChariotDbContext>(options => 
    options.UseSqlServer(connectionString));
//FIRST Variant, it's will be change to Forever Running 
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RentalChariotDbContext>();
    Console.WriteLine(db.Database.CanConnect());
}
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthorization();
app.MapControllers();
app.Run();
