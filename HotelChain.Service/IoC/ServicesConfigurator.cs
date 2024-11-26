using AutoMapper;
using HotelChain.BL.Permissions.Provider;
using HotelChain.BL.Users.Manager;
using HotelChain.BL.Users.Provider;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository.Repositories;
using HotelChain.Repository.Repository;
using HotelChain.Service.Settings;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<PermissionEntity>>(x =>
            new Repository<PermissionEntity>(x.GetRequiredService<IDbContextFactory<HotelChainDbContext>>()));
        
        services.AddScoped(typeof(IRepository<UserEntity>), typeof(UsersRepository));
        services.AddScoped<IRepository<UserEntity>>(x =>
            new UsersRepository(x.GetRequiredService<IDbContextFactory<HotelChainDbContext>>()));

        services.AddScoped<IPermissionsProvider>(x =>
            new PermissionsProvider(x.GetRequiredService<IRepository<PermissionEntity>>(),
                x.GetRequiredService<IMapper>()));
        
        services.AddScoped<IUsersProvider>(x =>
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IRepository<PermissionEntity>>(),
                x.GetRequiredService<IMapper>()));
    }
}