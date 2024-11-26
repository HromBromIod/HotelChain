using AutoMapper;
using HotelChain.BL.Permissions.Exceptions;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Exceptions;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository.Repository;

namespace HotelChain.BL.Users.Manager;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IRepository<PermissionEntity> _permissionsRepository;
    private readonly IMapper _mapper;

    public UsersManager(IRepository<UserEntity> usersRepository, IRepository<PermissionEntity> permissionsRepository,
        IMapper mapper)
    {
        _usersRepository = usersRepository;
        _permissionsRepository = permissionsRepository;
        _mapper = mapper;
    }

    public UserModel CreateUser(CreateUserModel createModel)
    {
        try
        {
            var entity = _mapper.Map<UserEntity>(createModel, opts: (x) =>
            {
                x.AfterMap((_, y) =>
                {
                    y.Permissions = [];
                });
            });
            entity = _usersRepository.Save(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new UserAlreadyExistsException("Пользователь с такими данными уже существует или введены некорректные данные");
        }
    }

    public void DeleteUser(int id)
    {
        var entity = _usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");

        _usersRepository.Delete(entity);
    }

    public UserModel UpdateUser(int id, UpdateUserModel updateModel)
    {
        var entity = _usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");

        entity = _mapper.Map<UpdateUserModel, UserEntity>(updateModel, opts => opts.AfterMap(
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
            entity = _usersRepository.Save(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new UserAlreadyExistsException("Пользователь с такими данными уже существует");
        }
    }

    public UserModel UpdateUsersPermissions(int id, UpdateUsersPermissionsModel updateModel)
    {
        var entity = _usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");
        
        var permissions = new List<PermissionEntity>();
        foreach (var permissionId in updateModel.Permissions)
        {
            var permissionEntity = _permissionsRepository.GetById(permissionId);
            if (permissionEntity is not null)
                permissions.Add(permissionEntity);
        }
        if (permissions.Count == 0)
            throw new PermissionNotFoundException("Таких прав доступа не существует");

        entity.Permissions = permissions;
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}