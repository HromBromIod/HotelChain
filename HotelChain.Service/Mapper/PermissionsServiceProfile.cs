using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.Service.Controllers.Permissions.Entities;

namespace HotelChain.Service.Mapper;

public class PermissionsServiceProfile : Profile
{
    public PermissionsServiceProfile()
    {
        CreateMap<FilterPermission, FilterPermissionModel>();
    }
}