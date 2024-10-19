using HotelChain.Service.IoC;

namespace HotelChain.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        SwaggerConfigurator.ConfigureServices(builder);
        SerilogConfigurator.ConfigureServices(builder);
        DbContextConfigurator.ConfigureServices(builder);
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SwaggerConfigurator.ConfigureApplication(app);
        SerilogConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
    }
}