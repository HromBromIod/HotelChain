namespace HotelChain.Service.Settings;

public class HotelChainSettings
{
    public string HotelChainDbContextConnectionString { get; set; }
    public string IdentityServerUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ApiName { get; set; }
    public (string UserName, string Password) MasterAdminData { get; set; }
}