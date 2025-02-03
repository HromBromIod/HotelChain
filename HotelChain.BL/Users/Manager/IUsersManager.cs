using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Users.Manager;

public interface IUsersManager
{
    Task DeleteUserAsync(int id);
    Task<UserModel> UpdateUserAsync(int id, UpdateUserModel updateModel);
    Task<UserModel> UpdateUsersPermissionsAsync(int id, UpdateUsersPermissionsModel updateModel);
}