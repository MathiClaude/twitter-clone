using Microsoft.EntityFrameworkCore;
using TwitterClone.Application.Dtos;
using TwitterClone.Domain.Common;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.Application.Services;

public class TweetService(ApplicationDbContext context)
{
    public async Task<Result> CreateTweet(Guid userId, string content)
    {
        try
        {
            var newTweet = new Tweet
            {
                UserId = userId,
                Content = content
            };

            context.Tweets.Add(newTweet);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("No se pudo crear el tweet");
        }
    }

    public async Task<Result<List<FeedResponseDto>>> GetUserFeed(Guid userId)
    {
        try
        {
            var userInfo = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => 
                    new { FollowersIds = u.Followers.Select(f => f.Id), FollowingIds = u.Following.Select(f => f.Id) })
                .FirstOrDefaultAsync();

            if (userInfo is null)
            {
                return Result<List<FeedResponseDto>>.Failure("Usuario no Econtrado", 404);
            }
            
            var data = await context.Tweets
                .Where(t => t.UserId == userId || userInfo.FollowersIds.Contains(t.UserId) ||
                            userInfo.FollowingIds.Contains(t.UserId))
                .Select(u => new FeedResponseDto(u.Id, u.User.UserName, u.User.Name, u.Content, u.CreatedAt, u.UserId))
                .ToListAsync();

            return Result<List<FeedResponseDto>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<List<FeedResponseDto>>.Failure("No se pudo obtener el feed");
        }
    }
}