using Microsoft.EntityFrameworkCore;
using TwitterClone.Application.Dtos;
using TwitterClone.Domain.Common;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.Application.Services;

public class FollowersService(ApplicationDbContext context)
{
    public async Task<Result<FollowersInfoResponseDto>> GetFollowCounts(Guid userId)
    {
        try
        {
            var followers = await context.Users
                .Include(u => u.Followers)
                .Where(u => u.Id == userId)
                .Select(u => u.Followers.Count)
                .FirstOrDefaultAsync();
        
            var following = await context.Users
                .Include(u => u.Following)
                .Where(u => u.Id == userId)
                .Select(u => u.Following.Count)
                .FirstOrDefaultAsync();

            return Result<FollowersInfoResponseDto>.Success(new FollowersInfoResponseDto(followers, following));
        }
        catch (Exception e)
        {
            return Result<FollowersInfoResponseDto>.Failure("No se pudo obtener la información de seguidores");
        }
    } 
    
    public async Task<Result<List<FollowersCountResponseDto>>> GetFollowingInfo(Guid userId)
    {
        try
        {
            var data = await context.Users
                .Include(u => u.Following)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Following)
                .Select(u => new FollowersCountResponseDto(u.Id, u.UserName, u.Name))
                .ToListAsync();

            return Result<List<FollowersCountResponseDto>>.Success(data.OrderBy(d => d.Name).ToList());
        }
        catch (Exception e)
        {
            return Result<List<FollowersCountResponseDto>>.Failure("No se pudo obtener la información de seguidores");
        }
    }
    
    public async Task<Result<List<FollowersCountResponseDto>>> GetFollowersInfo(Guid userId)
    {
        try
        {
            var data = await context.Users
                .Include(u => u.Followers)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Followers)
                .Select(u => new FollowersCountResponseDto(u.Id, u.UserName, u.Name))
                .ToListAsync();

            return Result<List<FollowersCountResponseDto>>.Success(data.OrderBy(d => d.Name).ToList());
        }
        catch (Exception e)
        {
            return Result<List<FollowersCountResponseDto>>.Failure("No se pudo obtener la información de seguidores");
        }
    }
    
    public async Task<Result> FollowUser(Guid userId, Guid followerId)
    {
        try
        {
            var user = await context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            
            var follower = await context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Where(u => u.Id == followerId)
                .FirstOrDefaultAsync();
            
            user.Followers.Add(follower);
            follower.Following.Add(user);
            
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("No se pudo seguir al usuario");
        }
    }
}