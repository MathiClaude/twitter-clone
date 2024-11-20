using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Utils;
using TwitterClone.Application.Services;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TweetController(TweetService service) :ControllerBase
{
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> CreateTweet(Guid userId, string content)
    {
        var result = await service.CreateTweet(userId, content);
        return result.ToActionResult();
    }
    
    [HttpGet("{userId:guid}/feed")]
    public async Task<IActionResult> GetUserFeed(Guid userId)
    {
        var feed = await service.GetUserFeed(userId);
        return feed.ToActionResult();
    }
}