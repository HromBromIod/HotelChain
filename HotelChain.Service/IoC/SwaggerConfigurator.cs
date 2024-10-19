namespace HotelChain.Service.IoC;

public static class SwaggerConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}