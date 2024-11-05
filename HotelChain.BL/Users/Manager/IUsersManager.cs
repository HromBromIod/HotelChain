using HotelChain.BL.Users.Entity;

namespace HotelChain.BL.Users.Manager;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel createModel);
    void DeleteUser(int id);
    UserModel UpdateUserModel(UpdateUserModel updateModel);
}