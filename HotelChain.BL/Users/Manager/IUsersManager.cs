using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Users.Manager;

public interface IUsersManager
{
    void DeleteUser(int id);
    UserModel UpdateUser(int id, UpdateUserModel updateModel);
    UserModel UpdateUsersPermissions(int id, UpdateUsersPermissionsModel updateModel);
}