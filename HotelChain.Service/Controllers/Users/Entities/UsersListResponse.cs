using HotelChain.BL.Users.Entity;

namespace HotelChain.Service.Controllers.Users.Entities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}