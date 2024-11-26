using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using HotelChain.Service.Settings;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.Service.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureServices(IServiceCollection services, HotelChainSettings settings)
    {
        var connectionString = settings.HotelChainDbContextConnectionString;
        services.AddDbContextFactory<HotelChainDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<HotelChainDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
        if (!context.Permissions.Any(p => p.Type == "PrivateRead"))
        {
            context.Permissions.Add(new PermissionEntity()
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateRead"
            });
            context.SaveChanges();
        }
        if (!context.Permissions.Any(p => p.Type == "PrivateWrite"))
        {
            context.Permissions.Add(new PermissionEntity()
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateWrite"
            });
            context.SaveChanges();
        }
        if (!context.Permissions.Any(p => p.Type == "PublicRead"))
        {
            context.Permissions.Add(new PermissionEntity()
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicRead"
            });
            context.SaveChanges();
        }
        if (!context.Permissions.Any(p => p.Type == "PublicWrite"))
        {
            context.Permissions.Add(new PermissionEntity()
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicWrite"
            });
            context.SaveChanges();
        }
    }
}