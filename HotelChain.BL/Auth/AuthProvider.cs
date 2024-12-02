using AutoMapper;
using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Exceptions.AuthExceptions;
using HotelChain.BL.Exceptions.UsersExceptions;
using HotelChain.BL.Users.Entity;
using HotelChain.DataAccess.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace HotelChain.BL.Auth;

public class AuthProvider(
    SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    IMapper mapper,
    string identityServerUri,
    string clientId,
    string clientSecret)
    : IAuthProvider
{
    public async Task<UserModel> RegisterUser(RegisterUserModel model)
    {
        var user = await userManager.FindByNameAsync(model.UserName);
        if (user is not null)
        {
            throw new UserAlreadyExistsException("Пользователь с таким именем уже существует");
        }

        user = mapper.Map<UserEntity>(model);
        user.ExternalId = Guid.NewGuid();
        user.CreationTime = DateTime.UtcNow;
        user.ModificationTime = DateTime.UtcNow;

        var createResult = await userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            throw new WrongCreationUserDataException(createResult.Errors.Select(x => x.Description)
                                                                        .Aggregate((x, y) => x + " " + y));
        }

        user = await userManager.FindByNameAsync(model.UserName);
        return mapper.Map<UserModel>(user);
    }

    public async Task<TokensResponse> AuthorizeUser(AuthorizeUserModel model)
    {
        var userByName = await userManager.FindByNameAsync(model.UserName);
        if (userByName is null)
        {
            throw new UserNotFoundException("Пользователя с такими данными не существует");
        }

        var checkPasswordResult = await signInManager.CheckPasswordSignInAsync(userByName, model.Password, false);
        if (!checkPasswordResult.Succeeded)
        {
            throw new WrongPasswordException("Неверный пароль");
        }

        var client = httpClientFactory.CreateClient();
        var endpoints = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (endpoints.IsError)
        {
            throw new Exception(endpoints.Error);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = endpoints.TokenEndpoint,
            ClientId = clientId,
            ClientSecret = clientSecret,
            UserName = model.UserName,
            Password = model.Password,
            Scope = "api offline_access"
        });
        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }
}