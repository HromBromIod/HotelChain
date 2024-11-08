namespace HotelChain.Service.Settings;

public static class HotelChainSettingsReader
{
    public static HotelChainSettings Read(IConfiguration configuration)
    {
        return new HotelChainSettings()
        {
            ServiceUri = configuration.GetValue<Uri>("Uri"),
            HotelChainDbContextConnectionString = configuration.GetValue<string>("HotelChainDbContext")
        };
    }
}