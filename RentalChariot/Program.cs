using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalChariot.Db;
using RentalChariot.UserManagement;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //This var for getting DataBase string from dotnet user-secrets,
                                                                                       //Huj ego znaet I dalbaeb skinul v pablic paroli, nu i huj z nim
builder.Services.AddDbContext<RentalChariotDbContext>(options => //Bludskaya Hueta nie soedeniatsa kak na Leksii Sharfika. Hueta trebuet ety ZALUPu  
    options.UseSqlServer(connectionString));
//FIRST Variant, it's will be change to Forever Running 
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RentalChariotDbContext>();
    Console.WriteLine(db.Database.CanConnect());
    //EXAMPLE OF ADDING NEW USER
    //var user = new User { Name = "TestName", Password = "TestPass" };
    //db.Users.Add(user);
    //db.SaveChanges();
    //var admin = new Admin { Name = "AdminTestName", Password = "AdminTestPass" };
    //db.Users.Add(admin);
    //db.SaveChanges();
}
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
