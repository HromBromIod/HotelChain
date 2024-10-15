namespace HotelChain.Service.IoC;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        SwaggerConfigurator.ConfigureServices(builder.Services);
        SerilogConfigurator.ConfigureServices(builder);
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SwaggerConfigurator.ConfigureApplication(app);
        SerilogConfigurator.ConfigureApplication(app);
    }
}