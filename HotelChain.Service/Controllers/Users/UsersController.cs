using AutoMapper;
using HotelChain.BL.Exceptions.PermissionsExceptions;
using HotelChain.BL.Exceptions.UsersExceptions;
using HotelChain.BL.Permissions.Entity;
using HotelChain.BL.Permissions.Provider;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Manager;
using HotelChain.BL.Users.Provider;
using HotelChain.Service.Controllers.Users.Entities;
using HotelChain.Service.Validator.User;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController(
    IUsersManager usersManager,
    IUsersProvider usersProvider,
    IPermissionsProvider permissionsProvider,
    IMapper mapper,
    ILogger logger)
    : ControllerBase
{
    [HttpPost]
    [Route("update")]
    public IActionResult UpdateUserInfo([FromQuery] UpdateUserRequest request)
    {
        var validationResult = new UpdateUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updateUserModel = mapper.Map<UpdateUserModel>(request);
            try
            {
                var userModel = usersManager.UpdateUser(request.Id, updateUserModel);
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
                logger.Error(e.ToString());
                return BadRequest("Что-то пошло не так, повторите позже)");
            }
        }

        logger.Error(validationResult.ToString());
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
                    Permissions = permissionsProvider.GetPermissions(new FilterPermissionModel
                    {
                        Types = request.Permissions
                    }).Select(x => x.Id).ToList()
                };

                var userModel = usersManager.UpdateUsersPermissions(request.Id, updateModel);
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
                logger.Error(e.ToString());
                return BadRequest("Что-то пошло не так, повторите позже)");
            }
        }

        logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpDelete]
    [Route("unregister")]
    public IActionResult UnregisterUser([FromQuery] int userIdToUnregister)
    {
        try
        {
            usersManager.DeleteUser(userIdToUnregister);
            return Ok("Пользователь был удален");
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Упс, что-то пошло не так. Повторите позже");
        }
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = usersProvider.GetUsers();
            return Ok(new UsersListResponse
            {
                Users = users.ToList()
            });
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так. Повторите попытку позже.");
        }
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        try
        {
            var userFilterModel = mapper.Map<FilterUserModel>(filter);
            var users = usersProvider.GetUsers(userFilterModel);
            return Ok(new UsersListResponse
            {
                Users = users.ToList()
            });
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так. Повторите попытку позже.");
        }
    }

    [HttpGet]
    [Route("info")]
    public IActionResult GetUserInfo([FromQuery] int id)
    {
        try
        {
            var userModel = usersProvider.GerUserInfo(id);
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
            logger.Error(e.ToString());
            return BadRequest(e.Message);
        }
    }
}