using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ScarletScreen.Model;
using ScarletScreen.Services;

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
    options.UseSqlServer("Server=GABES-PC\\MSSQLSERVER01;Database=Accounts;Trusted_Connection=True;TrustServerCertificate=True;");
});

// Configure Search Services
builder.Services.AddScoped<MongoDBService>();
builder.Services.AddScoped<TMDbService>();

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
        name: "home",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "movieDetails",
        pattern: "movies/{tmdb_id}",
        defaults: new { controller = "Movie", action = "Details" });
    endpoints.MapControllerRoute(
        name: "search",
        pattern: "/search/{query?}",
        defaults: new { controller = "Home", action = "Search" });
    endpoints.MapRazorPages();
});

app.Run();
