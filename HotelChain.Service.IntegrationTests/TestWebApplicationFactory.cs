using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HotelChain.Service.IntegrationTests;

public class TestWebApplicationFactory(Action<IServiceCollection>? overrideDependencies = null)
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => overrideDependencies?.Invoke(services));
    }
}