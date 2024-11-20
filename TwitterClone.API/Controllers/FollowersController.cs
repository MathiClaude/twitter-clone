using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Utils;
using TwitterClone.Application.Services;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowersController(FollowersService service) :ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetFollowersCount(Guid userId)
    {
        var followersCount = await service.GetFollowCounts(userId);
        return followersCount.ToActionResult();
    }
    
    [HttpGet("following/{userId:guid}")]
    public async Task<IActionResult> GetFollowingInfo(Guid userId)
    {
        var followingInfo = await service.GetFollowingInfo(userId);
        return followingInfo.ToActionResult();
    }
    
    [HttpGet("followers/{userId:guid}")]
    public async Task<IActionResult> GetFollowersInfo(Guid userId)
    {
        var followersInfo = await service.GetFollowersInfo(userId);
        return followersInfo.ToActionResult();
    }
    
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> FollowUser(Guid userId, Guid followId)
    {
        var result = await service.FollowUser(userId, followId);
        return result.ToActionResult();
    }
}