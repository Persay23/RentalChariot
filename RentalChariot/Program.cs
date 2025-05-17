using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalChariot.Db;
using RentalChariot.UserManagement;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Maybe in Future I will take DefaultConnection from appsetting.json
//Database link is Here
var dbOptionsBuilder = new DbContextOptionsBuilder<RentalChariotDbContext>();
dbOptionsBuilder.UseSqlServer("Server=tcp:databasevoks.database.windows.net,1433;Initial Catalog=RentalChariot;User ID=admin1;Password=Password1;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
var db = new RentalChariotDbContext(dbOptionsBuilder.Options);
Console.WriteLine(db.Database.CanConnect());


//TEST
var user = new User { name = "Test1", password = "Test2" };


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<RentalChariotDbContext>();

//    bool canConnect = context.Database.CanConnect();
//    Console.WriteLine(canConnect ? "Connected to DB!" : "ALARM ALARM ALARM ALARM: — ¿∆»“≈ ¬» “Œ–” ¬¿ÿ IP ◊“Œ¡€ œŒƒ Àﬁ◊»“‹—ﬂ.");
//}


app.Run();
