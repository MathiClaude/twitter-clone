using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Application.Dtos;

public record RegisterUserDto(string UserName, [EmailAddress] string Email, string Name, string Password);
public record LoginUserDto(string UserName, string Password);

public record UserInfoDto(Guid UserId, string UserName, string Name, string Email);