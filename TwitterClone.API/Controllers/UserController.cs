using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Utils;
using TwitterClone.Application.Dtos;
using TwitterClone.Application.Services;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto user)
    {
        var result = await userService.RegisterUser(user);
        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto user)
    {
        var loginUser = await userService.LoginUser(user);
        return loginUser.ToActionResult();
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserInfo(Guid userId)
    {
        var userInfo = await userService.GetUserInfo(userId);
        return userInfo.ToActionResult();
    }
}   