using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Users.Provider;

public interface IUsersProvider
{
    Task<IEnumerable<UserModel>> GetUsersAsync(FilterUserModel filter = null);
    Task<UserModel> GerUserInfoAsync(int id);
}