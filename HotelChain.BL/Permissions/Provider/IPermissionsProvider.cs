using HotelChain.BL.Permissions.Entity;

namespace HotelChain.BL.Permissions.Provider;

public interface IPermissionsProvider
{
    IEnumerable<PermissionModel> GetPermissions(FilterPermissionModel filter = null);
}