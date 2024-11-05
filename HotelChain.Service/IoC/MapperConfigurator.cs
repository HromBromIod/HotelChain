using HotelChain.BL.Mapper;

namespace HotelChain.Service.IoC;

public class MapperConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
        });
    }
}