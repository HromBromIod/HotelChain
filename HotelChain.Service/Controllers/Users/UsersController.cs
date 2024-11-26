using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.BL.Permissions.Exceptions;
using HotelChain.BL.Permissions.Provider;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Exceptions;
using HotelChain.BL.Users.Manager;
using HotelChain.BL.Users.Provider;
using HotelChain.Service.Controllers.Users.Entities;
using HotelChain.Service.Validator.User;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;
    private readonly IPermissionsProvider _permissionsProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider,
        IPermissionsProvider permissionsProvider, IMapper mapper, ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _permissionsProvider = permissionsProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var createUserModel = _mapper.Map<CreateUserModel>(request);
                var userModel = _usersManager.CreateUser(createUserModel);
                return Ok(new UsersListResponse
                {
                    Users = [userModel]
                });
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                return BadRequest(e.Message);
            }
        }

        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpPost]
    [Route("update")]
    public IActionResult UpdateUserInfo([FromQuery] UpdateUserRequest request)
    {
        var validationResult = new UpdateUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updateUserModel = _mapper.Map<UpdateUserModel>(request);
            try
            {
                var userModel = _usersManager.UpdateUser(request.Id, updateUserModel);
                return Ok(new UsersListResponse
                {
                    Users = [userModel]
                });
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                return BadRequest("Что-то пошло не так, повторите позже)");
            }
        }

        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpPost]
    [Route("permissions")]
    public IActionResult UpdateUsersPermissions([FromBody] UpdateUsersPermissionsRequest request)
    {
        var validationResult = new UpdateUsersPermissionsRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var updateModel = new UpdateUsersPermissionsModel
                {
                    Permissions = _permissionsProvider.GetPermissions(new FilterPermissionModel
                    {
                        Types = request.Permissions
                    }).Select(x => x.Id).ToList()
                };
                
                var userModel = _usersManager.UpdateUsersPermissions(request.Id, updateModel);
                return Ok(new UsersListResponse
                {
                    Users = [userModel]
                });
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (PermissionNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                return BadRequest("Что-то пошло не так, повторите позже)");
            }
        }

        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpDelete]
    [Route("unregister")]
    public IActionResult UnregisterUser([FromQuery] int userIdToUnregister)
    {
        try
        {
            _usersManager.DeleteUser(userIdToUnregister);
            return Ok("Пользователь был удален");
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest("Упс, что-то пошло не так. Повторите позже");
        }
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _usersProvider.GetUsers();
            return Ok(new UsersListResponse
            {
                Users = users.ToList()
            });
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так. Повторите попытку позже.");
        }
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        try
        {
            var userFilterModel = _mapper.Map<FilterUserModel>(filter);
            var users = _usersProvider.GetUsers(userFilterModel);
            return Ok(new UsersListResponse
            {
                Users = users.ToList()
            });
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так. Повторите попытку позже.");
        }
    }

    [HttpGet]
    [Route("info")]
    public IActionResult GetUserInfo([FromQuery] int id)
    {
        try
        {
            var userModel = _usersProvider.GerUserInfo(id);
            return Ok(new UsersListResponse()
            {
                Users = [userModel]
            });
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest(e.Message);
        }
    }
}