using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Exceptions;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository.Repository;

namespace HotelChain.BL.Users.Provider;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UsersProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetUsers(FilterUserModel filter = null)
    {
        string? loginPart = filter?.Login;
        string? namePart = filter?.Name;
        string? phoneNumberPart = filter?.PhoneNumber;
        string? emailPart = filter?.Email;
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        List<string>? permissions = filter?.Permissions;

        var users = _userRepository.GetAll(u =>
            (loginPart == null || u.UserName.Contains(loginPart)) &&
            (namePart == null || u.FullName.Contains(namePart)) &&
            (phoneNumberPart == null || u.PhoneNumber.Contains(phoneNumberPart)) &&
            (emailPart == null || u.Email.Contains(emailPart)) &&
            (creationTime == null || u.CreationTime == creationTime) &&
            (modificationTime == null || u.ModificationTime == modificationTime) &&
            (permissions == null || permissions.Any(p => u.Permissions.Any(x => x.Type.Contains(p)))));
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GerUserInfo(int id)
    {
        var entity = _userRepository.GetById(id);
        if (entity == null)
            throw new UserNotFoundException("User not found");
        
        return _mapper.Map<UserModel>(entity);
    }
}