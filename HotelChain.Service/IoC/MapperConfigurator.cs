using HotelChain.BL.Mapper;
using HotelChain.Service.Mapper;

namespace HotelChain.Service.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
            
            config.AddProfile<PermissionsBLProfile>();
            config.AddProfile<PermissionsServiceProfile>();
        });
    }
}