using AutoMapper;
using HotelChain.BL.Mapper;

namespace HotelChain.BL.UnitTests.Helpers;

public static class MapperHelper
{
    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(UsersBLProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }

    public static IMapper Mapper { get; }
}