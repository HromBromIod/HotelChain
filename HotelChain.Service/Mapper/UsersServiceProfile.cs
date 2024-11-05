using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.Service.Controllers.Entities.UserEntities;

namespace HotelChain.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UserFilter, UserFilterModel>();
        CreateMap<RegisterUserRequest, CreateUserModel>();
    }
}