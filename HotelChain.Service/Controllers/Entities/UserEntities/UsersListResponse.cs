using HotelChain.BL.Users.Entity;

namespace HotelChain.Service.Controllers.Entities.UserEntities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}