using AutoMapper;
using HotelChain.BL.Exceptions.PermissionsExceptions;
using HotelChain.BL.Exceptions.UsersExceptions;
using HotelChain.BL.Users.Entity;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;

namespace HotelChain.BL.Users.Manager;

public class UsersManager(
    IRepository<UserEntity> usersRepository,
    IRepository<PermissionEntity> permissionsRepository,
    IMapper mapper)
    : IUsersManager
{

    public void DeleteUser(int id)
    {
        var entity = usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");

        usersRepository.Delete(entity);
    }

    public UserModel UpdateUser(int id, UpdateUserModel updateModel)
    {
        var entity = usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");

        entity = mapper.Map<UpdateUserModel, UserEntity>(updateModel, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = entity.Id;
                dest.ExternalId = entity.ExternalId;
                dest.CreationTime = entity.CreationTime;
                dest.ModificationTime = entity.ModificationTime;
                dest.UserName = src.UserName == null ? entity.UserName : dest.UserName;
                dest.PasswordHash = src.PasswordHash == null ? entity.PasswordHash : dest.PasswordHash;
                dest.PassportSeries = src.PassportSeries == null ? entity.PassportSeries : dest.PassportSeries;
                dest.PassportNumber = src.PassportNumber == null ? entity.PassportNumber : dest.PassportNumber;
                dest.PhoneNumber = src.PhoneNumber == null ? entity.PhoneNumber : dest.PhoneNumber;
                dest.Email = src.Email == null ? entity.Email : dest.Email;
                dest.FullName = src.FullName == null ? entity.FullName : dest.FullName;
                dest.BirthDate = src.BirthDate == null ? entity.BirthDate : dest.BirthDate;
                dest.Permissions = entity.Permissions;
            }));
        try
        {
            entity = usersRepository.Save(entity);
            return mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new UserAlreadyExistsException("Пользователь с такими данными уже существует");
        }
    }

    public UserModel UpdateUsersPermissions(int id, UpdateUsersPermissionsModel updateModel)
    {
        var entity = usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");
        
        var permissions = new List<PermissionEntity>();
        foreach (var permissionId in updateModel.Permissions)
        {
            var permissionEntity = permissionsRepository.GetById(permissionId);
            if (permissionEntity is not null)
                permissions.Add(permissionEntity);
        }
        if (permissions.Count == 0)
            throw new PermissionNotFoundException("Таких прав доступа не существует");

        entity.Permissions = permissions;
        entity = usersRepository.Save(entity);
        return mapper.Map<UserModel>(entity);
    }
}