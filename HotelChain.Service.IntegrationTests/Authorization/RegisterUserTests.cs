using FluentAssertions;
using HotelChain.BL.Auth;
using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Manager;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;
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
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userModel = await authProvider.RegisterUser(registerUserModel);

        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var userEntity = userRepository.GetById(userModel.Id);

        userEntity.Should().NotBeNull();
        userEntity.UserName.Should().Be(registerUserModel.UserName);
        
        userRepository.Delete(userEntity);
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
        
        Func<Task<UserModel>> expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        
        var userManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        userManager.DeleteUser(userModel.Id);
    }

    //не проходят, тк забыл constraint на бд наложить - тесты помогли найти косяк!
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
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        
        registerUserModel.Password = "1";
        Func<Task<UserModel>> expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.Password = "abOba_0321";
        
        registerUserModel.PassportSeries = 1;
        expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.PassportSeries = 8989;
        
        registerUserModel.PassportNumber = 1;
        expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.PassportNumber = 898989;
        
        registerUserModel.PhoneNumber = "1";
        expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.PhoneNumber = "+78981112233";
        
        registerUserModel.Email = "1";
        expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.Email = "abobovich@gmail.com";
        
        registerUserModel.BirthDate = DateTime.UtcNow.AddYears(20);
        expectedAct = async () => await authProvider.RegisterUser(registerUserModel);
        await expectedAct.Should().ThrowAsync<Exception>();
        registerUserModel.BirthDate = DateTime.UtcNow.AddYears(-20);
    }
}