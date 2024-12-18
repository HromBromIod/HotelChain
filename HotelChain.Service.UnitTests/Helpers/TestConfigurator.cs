using HotelChain.Service.Settings;
using Microsoft.Extensions.Configuration;

namespace HotelChain.Service.UnitTests.Helpers;

public class TestConfigurator
{
    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
    
    public static HotelChainSettings GetSettings()
    {
        return HotelChainSettingsReader.Read(GetConfiguration());
    }
}