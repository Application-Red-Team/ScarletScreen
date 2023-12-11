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
var mongoConnectionString = builder.Configuration.GetConnectionString("DatabaseSettings:ConnectionString");
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabaseName = builder.Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
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

app.Run();
