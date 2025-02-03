using AutoMapper;
using HotelChain.BL.Permissions.Entity;
using HotelChain.BL.Permissions.Provider;
using HotelChain.Service.Controllers.Permissions.Entities;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers.Permissions;

[ApiController]
[Route("[controller]")]
public class PermissionController(
    IPermissionsProvider permissionsProvider,
    IMapper mapper,
    ILogger logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        try
        {
            var permissions = await permissionsProvider.GetPermissionsAsync();
            return Ok(new PermissionListResponse
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
    
    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetFilteredPermissions([FromQuery] FilterPermission filter)
    {
        try
        {
            var filterModel = mapper.Map<FilterPermissionModel>(filter);
            var permissions = await permissionsProvider.GetPermissionsAsync(filterModel);
            return Ok(new PermissionListResponse
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
}