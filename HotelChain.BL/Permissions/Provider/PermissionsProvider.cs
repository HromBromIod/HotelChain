using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository.Repository;

namespace HotelChain.BL.Permissions.Provider;

public class PermissionsProvider : IPermissionsProvider
{
    private IRepository<PermissionEntity> _permissionsRepository;
    private IMapper _mapper;

    public PermissionsProvider(IRepository<PermissionEntity> permissionsRepository, IMapper mapper)
    {
        _permissionsRepository = permissionsRepository;
        _mapper = mapper;
    }

    public IEnumerable<PermissionModel> GetPermissions(FilterPermissionModel filter = null)
    {
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        List<string>? typeParts = filter?.Types;

        var permissions = _permissionsRepository.GetAll(p =>
            (creationTime == null || p.CreationTime == creationTime) &&
            (modificationTime == null || p.ModificationTime == modificationTime) &&
            (typeParts == null || typeParts.Any(x => p.Type.Contains(x))));
        return _mapper.Map<IEnumerable<PermissionModel>>(permissions);
    }
}