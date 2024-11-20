namespace TwitterClone.Application.Dtos;

public record FeedResponseDto(Guid TweetId, string UserName, string Name, string Text, DateTime CreatedAt);