using AutoMapper;
using HotelChain.BL.Auth.Entities;
using HotelChain.DataAccess.Entities;

namespace HotelChain.BL.Mapper;

public class AuthBLProfile : Profile
{
    public AuthBLProfile()
    {
        CreateMap<RegisterUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.FullName,
                y => y.MapFrom(src =>
                    src.Surname + " " + src.Name + (src.Patronymic == null ? string.Empty : " " + src.Patronymic)))
            .ForMember(x => x.Permissions, y => y.Ignore());
    }
}