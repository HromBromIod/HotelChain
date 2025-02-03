using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;

namespace HotelChain.BL.Permissions.Provider;

public class PermissionsProvider(IRepository<PermissionEntity> permissionsRepository, IMapper mapper)
    : IPermissionsProvider
{
    public async Task<IEnumerable<PermissionModel>> GetPermissionsAsync(FilterPermissionModel? filter = null)
    {
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        List<string>? typeParts = filter?.Types;

        var permissions = await permissionsRepository.GetAllAsync(p =>
            (creationTime == null || p.CreationTime == creationTime) &&
            (modificationTime == null || p.ModificationTime == modificationTime) &&
            (typeParts == null || typeParts.Any(x => p.Type.Contains(x))));
        
        return mapper.Map<IEnumerable<PermissionModel>>(permissions);
    }
}