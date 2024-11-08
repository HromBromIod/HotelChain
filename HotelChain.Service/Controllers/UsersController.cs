using AutoMapper;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Manager;
using HotelChain.BL.Users.Provider;
using HotelChain.Service.Controllers.Entities.UserEntities;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider,
        IMapper mapper, ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    // [HttpPost]
    // public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    // {
    //     //var validateErrors = request.Validate();
    //
    //     return Ok();
    // }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersListResponse()
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromBody] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<UserFilterModel>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersListResponse()
        {
            Users = users.ToList()
        });
    }
}