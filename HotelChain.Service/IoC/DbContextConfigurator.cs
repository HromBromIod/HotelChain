using HotelChain.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.Service.IoC;

public class DbContextConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        string connectionString = configuration.GetValue<string>("HotelChainDbContext");

        builder.Services.AddDbContextFactory<HotelChainDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder  app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<HotelChainDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}