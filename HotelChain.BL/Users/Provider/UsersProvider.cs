using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Exceptions;
using HotelChain.DataAccess.Entities;
using HotelChain.DataAccess.Repository;

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

    public IEnumerable<UserModel> GetUsers(UserFilterModel filter = null)
    {
        string? loginPart = filter?.LoginPart;
        string? namePart = filter?.NamePart;
        string? phoneNumberPart = filter?.PhoneNumberPart;
        string? emailPart = filter?.EmailPart;
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        int? permission = filter?.Permission;

        var users = _userRepository.GetAll(u =>
            (loginPart == null || u.Login == loginPart) &&
            (namePart == null || u.FullName.Contains(namePart)) &&
            (phoneNumberPart == null || u.PhoneNumber.Contains(phoneNumberPart)) &&
            (emailPart == null || u.Email.Contains(emailPart)) &&
            (creationTime == null || u.CreationTime == creationTime) &&
            (modificationTime == null || u.ModificationTime == modificationTime) &&
            (permission == null || u.Permission.Id == permission)
        );
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GerUserInfo(int id)
    {
        var entity = _userRepository.GetById(id);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }

        return _mapper.Map<UserModel>(entity);
    }
}