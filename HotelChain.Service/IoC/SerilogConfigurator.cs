using Serilog;

namespace HotelChain.Service.IoC;

public static class SerilogConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.WithCorrelationId()
                .ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddHttpContextAccessor();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}