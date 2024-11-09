using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Users.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(FilterUserModel filter = null);
    UserModel GerUserInfo(int id);
}