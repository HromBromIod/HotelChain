using AutoMapper;
using HotelChain.BL.Auth.Entities;
using HotelChain.Service.Controllers.Auth.Entities;

namespace HotelChain.Service.Mapper;

public class AuthServiceProfile : Profile
{
    public AuthServiceProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserModel>();
        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>();
    }
}