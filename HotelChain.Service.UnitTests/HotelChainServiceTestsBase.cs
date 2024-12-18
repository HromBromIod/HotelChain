using Duende.IdentityServer.Models;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;
using HotelChain.Service.UnitTests.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace HotelChain.Service.UnitTests;

public class HotelChainServiceTestsBase
{
    private readonly WebApplicationFactory<Program> _testServer;
    protected HttpClient TestHttpClient => _testServer.CreateClient();

    public HotelChainServiceTestsBase()
    {
        var settings = TestConfigurator.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    public T GetService<T>() where T : notnull
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // using var scope = GetService<IServiceScopeFactory>().CreateScope();
        // var usersRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        // var user = usersRepository.Save(new UserEntity
        // {
        //     Email = "abcd@gmail.com",
        //     PhoneNumber = "+79093452551",
        //     UserName = "Aboba_1",
        //     PassportNumber = 8989,
        //     PassportSeries = 898989,
        //     PasswordHash = "password".Sha256(),
        //     FullName = "Abc Abc Abc",
        //     BirthDate = DateTime.UtcNow.AddYears(-20)
        // });
    }

    [OneTimeTearDownAttribute]
    public void OneTimeTearDown()
    {
        _testServer.Dispose();
    }
}