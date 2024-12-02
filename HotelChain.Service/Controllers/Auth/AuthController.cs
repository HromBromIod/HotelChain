using AutoMapper;
using HotelChain.BL.Auth;
using HotelChain.BL.Auth.Entities;
using HotelChain.Service.Controllers.Auth.Entities;
using HotelChain.Service.Controllers.Users.Entities;
using HotelChain.Service.Validator.User;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace HotelChain.Service.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthProvider authProvider, IMapper mapper, ILogger logger) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        try
        {
            var validationResult = await new RegisterUserRequestValidator().ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var registerModel = mapper.Map<RegisterUserModel>(request);
            
            var userModel = await authProvider.RegisterUser(registerModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите позже)");
        }
    }
    
    [HttpGet]
    [Route("authorize")]
    public async Task<IActionResult> AuthorizeUser([FromQuery] AuthorizeUserRequest request)
    {
        try
        {
            var authRequest = mapper.Map<AuthorizeUserModel>(request);

            var tokens = await authProvider.AuthorizeUser(authRequest);

            return Ok(tokens);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.Error(e.ToString());
            return BadRequest("Что-то пошло не так, повторите позже)");
        }
    }
}