using System.Net;
using FluentAssertions;
using HotelChain.BL.Auth;
using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Manager;
using HotelChain.Service.IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelChain.Service.IntegrationTests.Authorization;

public class AuthorizeUserTests : HotelChainServiceTestsBase
{
    [Test]
    public async Task HappyPathTest()
    {
        var registerUserModel = new RegisterUserModel
        {
            UserName = "Ab0b_a",
            Password = "abOba_0321",
            PassportSeries = 8989,
            PassportNumber = 898989,
            PhoneNumber = "+78981112233",
            Email = "abobovich@gmail.com",
            Name = "Bob",
            Surname = "Smith",
            Patronymic = null,
            BirthDate = DateTime.UtcNow.AddYears(-20)
        };
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userModel = await authProvider.RegisterUser(registerUserModel);
        
        var query = $"?username={registerUserModel.UserName}"
                    + $"&password={registerUserModel.Password}";
        var requestUri =
            HotelChainApiEndpoints.AuthorizeUserEndpoint + query;
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContentJson = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);
        
        content.Should().NotBeNull();
        content.AccessToken.Should().NotBeNull();
        content.RefreshToken.Should().NotBeNull();
        
        var usersManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        usersManager.DeleteUser(userModel.Id);
    }

    [Test]
    public async Task AuthorizeWithoutRegistrationTest()
    {
        const string userName = "Ab0b_a";
        const string password = "abOba_0321";
        
        const string query = $"?username={userName}"
                             + $"&password={password}";
        const string requestUri = HotelChainApiEndpoints.AuthorizeUserEndpoint + query;
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task AuthorizeWithWrongDataTest()
    {
        var registerUserModel = new RegisterUserModel
        {
            UserName = "Ab0b_a",
            Password = "abOba_0321",
            PassportSeries = 8989,
            PassportNumber = 898989,
            PhoneNumber = "+78981112233",
            Email = "abobovich@gmail.com",
            Name = "Bob",
            Surname = "Smith",
            Patronymic = null,
            BirthDate = DateTime.UtcNow.AddYears(-20)
        };

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userModel = await authProvider.RegisterUser(registerUserModel);
        
        const string wrongUserName = "ABBA";
        var wrongQuery1 = $"?username={wrongUserName}"
                    + $"&password={registerUserModel.Password}";
        var requestUri1 =
            HotelChainApiEndpoints.AuthorizeUserEndpoint + wrongQuery1;
        var request1 = new HttpRequestMessage(HttpMethod.Get, requestUri1);
        
        const string wrongPassword = "ABBA";
        var wrongQuery2 = $"?username={registerUserModel.UserName}"
                          + $"&password={wrongPassword}";
        var requestUri2 =
            HotelChainApiEndpoints.AuthorizeUserEndpoint + wrongQuery2;
        var request2 = new HttpRequestMessage(HttpMethod.Get, requestUri2);
        
        var client = TestHttpClient;
        
        var response = await client.SendAsync(request1);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        response = await client.SendAsync(request2);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var usersManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        usersManager.DeleteUser(userModel.Id);
    }
}