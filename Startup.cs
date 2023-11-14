using ScarletScreen.MongoConnection.Services;

public class Startup
    {
        public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add the DatabaseSettings configuration
        services.Configure<DatabaseSettings>(
            Configuration.GetSection(nameof(DatabaseSettings)));

        // Add the MongoDB context
        services.AddSingleton<MoviesDbContext>();
        services.AddSingleton<TVDbContext>();
        services.AddSingleton<TMDbService>();


        // Other service configurations...
    }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Add this if you're using authentication
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                // Add other endpoints as needed
            });
        }
    }