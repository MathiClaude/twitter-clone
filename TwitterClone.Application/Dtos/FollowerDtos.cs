namespace TwitterClone.Application.Dtos;

public record FollowersInfoResponseDto(int Followers, int Following);

public record FollowingCountResponseDto(int Following);

public record FollowersCountResponseDto(Guid UserId, string UserName, string Name);