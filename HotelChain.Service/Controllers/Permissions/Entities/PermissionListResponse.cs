using HotelChain.BL.Permissions.Entity;

namespace HotelChain.Service.Controllers.Permissions.Entities;

public class PermissionListResponse
{
    public List<PermissionModel> Permissions { get; set; }
}