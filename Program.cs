using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ScarletScreen.Model;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configuration for appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddRazorPages();

// Add MongoDB
var configuration = builder.Configuration.GetSection("DatabaseSettings");
var mongoConnectionString = configuration["ConnectionString"];
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabaseName = configuration["DatabaseName"];
builder.Services.AddSingleton<IMongoDatabase>(mongoClient.GetDatabase(mongoDatabaseName));

// Add SQL Server
builder.Services.AddDbContext<AccountsContext>(options =>
{
    options.UseSqlServer("Server=DESKTOP-Q9342B9;Database=Accounts;Trusted_Connection=True;TrustServerCertificate=True;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "movieDetails",
        pattern: "movies/{tmdb_id}",
        defaults: new { controller = "Movie", action = "Details" });
    endpoints.MapControllerRoute(
        name: "tvDetails",
        pattern: "tv/{tmdb_id}",
        defaults: new { controller = "TV", action = "Details" });

    endpoints.MapRazorPages();
});

app.Run();
