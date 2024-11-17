using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.Service.Controllers.Users.Entities;

namespace HotelChain.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserFilter, FilterUserModel>();
    }
}