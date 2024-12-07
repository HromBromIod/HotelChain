namespace HotelChain.Service.Settings;

public static class HotelChainSettingsReader
{
    public static HotelChainSettings Read(IConfiguration configuration)
    {
        return new HotelChainSettings
        {
            HotelChainDbContextConnectionString = configuration.GetValue<string>("HotelChainDbContext"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServer:Uri"),
            ClientId = configuration.GetValue<string>("IdentityServer:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServer:ClientSecret"),
            ApiName = configuration.GetValue<string>("IdentityServer:ApiName"),
            MasterAdminData = new (
                configuration.GetValue<string>("IdentityServer:MasterAdminData:UserName"),
                configuration.GetValue<string>("IdentityServer:MasterAdminData:Password"))
        };
    }
}