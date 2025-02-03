using AutoMapper;
using HotelChain.BL.Exceptions.UsersExceptions;
using HotelChain.BL.Users.Entity;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;

namespace HotelChain.BL.Users.Provider;

public class UsersProvider(IRepository<UserEntity> userRepository, IMapper mapper) : IUsersProvider
{
    public async Task<IEnumerable<UserModel>> GetUsersAsync(FilterUserModel? filter = null)
    {
        string? loginPart = filter?.Login;
        string? namePart = filter?.Name;
        string? phoneNumberPart = filter?.PhoneNumber;
        string? emailPart = filter?.Email;
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        List<string>? permissions = filter?.Permissions;

        var users = await userRepository.GetAllAsync(u =>
            (loginPart == null || u.UserName.Contains(loginPart)) &&
            (namePart == null || u.FullName.Contains(namePart)) &&
            (phoneNumberPart == null || u.PhoneNumber.Contains(phoneNumberPart)) &&
            (emailPart == null || u.Email.Contains(emailPart)) &&
            (creationTime == null || u.CreationTime == creationTime) &&
            (modificationTime == null || u.ModificationTime == modificationTime) &&
            (permissions == null || permissions.Any(p => u.Permissions.Any(x => x.Type.Contains(p)))));
        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    public async Task<UserModel> GerUserInfoAsync(int id)
    {
        var entity = await userRepository.GetByIdAsync(id);
        if (entity == null)
            throw new UserNotFoundException("User not found");
        
        return mapper.Map<UserModel>(entity);
    }
}