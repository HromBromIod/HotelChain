using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Exceptions;
using HotelChain.DataAccess.Entities;
using HotelChain.DataAccess.Repository;

namespace HotelChain.BL.Users.Manager;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;

    public UsersManager(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public UserModel CreateUser(CreateUserModel createModel)
    {
        //validation

        var entity = _mapper.Map<UserEntity>(createModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }

    public void DeleteUser(int id)
    {
        var entity = _usersRepository.GetById(id);
        try
        {
            _usersRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new UserNotFoundException(e.Message);
        }
    }

    public UserModel UpdateUserModel(UpdateUserModel updateModel)
    {
        //validation
        
        var entity = _mapper.Map<UserEntity>(updateModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}