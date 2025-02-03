using HotelChain.BL.Auth;
using HotelChain.BL.Auth.Entities;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Manager;
using HotelChain.BL.Users.Provider;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using HotelChain.Service.Settings;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.Service.IoC;

public static class RepositoryInitializer
{
    private static async Task<List<PermissionEntity>> InitializePermissions(
        IDbContextFactory<HotelChainDbContext> dbContextFactory)
    {
        var permissions = new List<PermissionEntity>();

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PrivateRead");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateRead"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PrivateWrite");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateWrite"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PublicRead");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicRead"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PublicWrite");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicWrite"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        await context.SaveChangesAsync();
        return permissions;
    }

    private static async Task<UserModel> CreateGlobalAdmin(IAuthProvider authProvider, string userName, string password)
    {
        return await authProvider.RegisterUser(new RegisterUserModel
        {
            UserName = userName,
            Password = password
        });
    }

    private static void GrantPermissions(IUsersManager usersManager, int id, List<PermissionEntity> permissions)
    {
        usersManager.UpdateUsersPermissionsAsync(id, new UpdateUsersPermissionsModel
        {
            Permissions = permissions.Select(x => x.Id).ToList()
        });
    }

    public static async Task ConfigureApplication(IApplicationBuilder app, HotelChainSettings settings)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var dbContextFactory =
            (IDbContextFactory<HotelChainDbContext>)scope.ServiceProvider.GetRequiredService(
                typeof(IDbContextFactory<HotelChainDbContext>));
        var permissions = await InitializePermissions(dbContextFactory);

        var usersProvider = (IUsersProvider)scope.ServiceProvider.GetRequiredService(typeof(IUsersProvider));
        if (!(await usersProvider.GetUsersAsync(new FilterUserModel { Login = settings.MasterAdminData.UserName })).Any())
        {
            var authProvider = (IAuthProvider)scope.ServiceProvider.GetRequiredService(typeof(IAuthProvider));
            var adminModel = await CreateGlobalAdmin(authProvider, settings.MasterAdminData.UserName,
                settings.MasterAdminData.Password);

            var usersManager = (IUsersManager)scope.ServiceProvider.GetRequiredService(typeof(IUsersManager));
            GrantPermissions(usersManager, adminModel.Id, permissions);
        }
    }
}