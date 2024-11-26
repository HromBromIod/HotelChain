using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.DataAccess.Entities;

namespace HotelChain.BL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.Permissions, y => y.MapFrom(src =>
                src.Permissions.Select(p => p.Type)));

        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.FullName,
                y => y.MapFrom(src =>
                    src.Surname + " " + src.Name + (src.Patronymic == null ? string.Empty : " " + src.Patronymic)))
            .ForMember(x => x.Permissions, y => y.Ignore());

        CreateMap<UpdateUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.UserName, y =>
                y.PreCondition(src => src.UserName is not null))
            .ForMember(x => x.UserName, y => y.MapFrom(src =>
                src.UserName))
            .ForMember(x => x.PasswordHash, y =>
                y.PreCondition(src => src.PasswordHash is not null))
            .ForMember(x => x.PassportSeries, y =>
                y.PreCondition(src => src.PassportSeries is not null))
            .ForMember(x => x.PassportNumber, y =>
                y.PreCondition(src => src.PassportNumber is not null))
            .ForMember(x => x.PhoneNumber, y =>
                y.PreCondition(src => src.PhoneNumber is not null))
            .ForMember(x => x.Email, y =>
                y.PreCondition(src => src.Email is not null))
            .ForMember(x => x.FullName, y =>
                y.PreCondition(src => src.FullName is not null))
            .ForMember(x => x.BirthDate, y =>
                y.PreCondition(src => src.BirthDate is not null));

        CreateMap<CreateUserModel, FilterUserModel>();
    }
}