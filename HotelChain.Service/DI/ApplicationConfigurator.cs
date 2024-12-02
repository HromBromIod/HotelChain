using HotelChain.Service.IoC;
using HotelChain.Service.Settings;

namespace HotelChain.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, HotelChainSettings settings)
    {
        SerilogConfigurator.ConfigureServices(builder);
        DbContextConfigurator.ConfigureServices(builder.Services, settings);
        AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);

        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        AuthorizationConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);

        app.MapControllers();
    }
}