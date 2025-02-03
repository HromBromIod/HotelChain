using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.DataAccess.Entities;

namespace HotelChain.BL.Mapper;

public class PermissionsBLProfile : Profile
{
    public PermissionsBLProfile()
    {
        CreateMap<PermissionEntity, PermissionModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id));
    }
}