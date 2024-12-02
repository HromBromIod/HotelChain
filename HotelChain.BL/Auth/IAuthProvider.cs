using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Auth;

public interface IAuthProvider
{
    Task<UserModel> RegisterUser(RegisterUserModel model);
    Task<TokensResponse> AuthorizeUser(AuthorizeUserModel model);
}