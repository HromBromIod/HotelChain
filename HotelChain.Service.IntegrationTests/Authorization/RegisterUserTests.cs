using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HotelChain.BL.Auth;
using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Manager;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;
using HotelChain.Service.IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace HotelChain.Service.IntegrationTests.Authorization;

public class RegisterUserTests : HotelChainServiceTestsBase
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
        
        
        var client = TestHttpClient;
        var response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var userEntities = userRepository.GetAll(e => e.UserName == registerUserModel.UserName)
            .ToList();
        userRepository.Delete(userEntities[0]);
    }

    [Test]
    public async Task RegisterUserThatAlreadyExistsTest()
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
        
        var client = TestHttpClient;
        var response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var userManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        userManager.DeleteUser(userModel.Id);
    }

    [Test]
    public async Task RegisterUserWithWrongData()
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
        
        var client = TestHttpClient;
        
        registerUserModel.Password = "1";
        var response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.Password = "abOba_0321";
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        registerUserModel.PassportSeries = 1;
        response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.PassportSeries = 8989;
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        registerUserModel.PassportNumber = 1;
        response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.PassportNumber = 898989;
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        
        registerUserModel.PhoneNumber = "1";
        response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.PhoneNumber = "+78981112233";
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        registerUserModel.Email = "1";
        response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.Email = "abobovich@gmail.com";
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        registerUserModel.BirthDate = DateTime.UtcNow.AddYears(20);
        response = await client.PostAsJsonAsync(HotelChainApiEndpoints.RegisterUserEndpoint, registerUserModel);
        registerUserModel.BirthDate = DateTime.UtcNow.AddYears(-20);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}