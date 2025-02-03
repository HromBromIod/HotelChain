using HotelChain.BL.Permissions.Entity;

namespace HotelChain.BL.Permissions.Provider;

public interface IPermissionsProvider
{
    Task<IEnumerable<PermissionModel>> GetPermissionsAsync(FilterPermissionModel filter = null);
}