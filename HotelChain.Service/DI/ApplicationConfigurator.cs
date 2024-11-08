using HotelChain.Service.IoC;
using HotelChain.Service.Settings;

namespace HotelChain.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, HotelChainSettings settings)
    {
        DbContextConfigurator.ConfigureServices(builder.Services, settings);
        SerilogConfigurator.ConfigureServices(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);
        
        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}