namespace HotelChain.Service.Controllers.Users.Entities;

public class UpdateUsersPermissionsRequest
{
    public int Id { get; set; }
    public List<string> Permissions { get; set; }
}