using Microsoft.EntityFrameworkCore;
using RentalChariot.Db;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<RentalChariotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RentalChariotDbContext>();

    bool canConnect = context.Database.CanConnect();
    Console.WriteLine(canConnect ? "Connected to DB!" : "ALARM ALARM ALARM ALARM: — ¿∆»“≈ ¬» “Œ–” ¬¿ÿ IP ◊“Œ¡€ œŒƒ Àﬁ◊»“‹—ﬂ.");
}

app.Run();
