using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.BL.Permissions.Provider;
using HotelChain.Service.Controllers.Permissions.Entities;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers.Permissions;

[ApiController]
[Route("[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionsProvider _permissionsProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public PermissionController(IPermissionsProvider permissionsProvider,
        IMapper mapper, ILogger logger)
    {
        _permissionsProvider = permissionsProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        try
        {
            var permissions = _permissionsProvider.GetPermissions();
            return Ok(new PermissionListResponse()
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
    
    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredPermissions([FromQuery] FilterPermission filter)
    {
        try
        {
            var filterModel = _mapper.Map<FilterPermissionModel>(filter);
            var permissions = _permissionsProvider.GetPermissions(filterModel);
            return Ok(new PermissionListResponse()
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
}