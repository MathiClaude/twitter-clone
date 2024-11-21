using Microsoft.EntityFrameworkCore;
using TwitterClone.API.Utils;
using TwitterClone.Application.Dtos;
using TwitterClone.Domain.Common;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.Application.Services;

public class UserService(ApplicationDbContext context)
{
    public async Task<Result> RegisterUser(RegisterUserDto user)
    {
        try
        {
            
            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = PasswordService.HashPassword(user.Password),
                Name = user.Name
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("No se pudo registrar el usuario");
        }
    
    }       
    
    public async Task<Result<UserInfoDto>> LoginUser(LoginUserDto user)
    {
        try
        {
            var loginUser = await context.Users
                .FirstOrDefaultAsync(x => x.UserName == user.UserName);
            //usuario no encontrado
            if (loginUser is null)
            {
                return Result<UserInfoDto>.Failure("Usuario no encontrado", 404);
            }
            if(!PasswordService.VerifyPassword(loginUser.Password, user.Password))
            {
                return Result<UserInfoDto>.Failure("Credenciales incorrectas", 401);
            }
            
            /**
             * Aqui podemos crear un token para el usuario y devolverlo en el resultado
             */
            
            return Result<UserInfoDto>.Success(new UserInfoDto(loginUser.Id, loginUser.UserName, loginUser.Name, loginUser.Email));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<UserInfoDto>.Failure("No se pudo iniciar sesión");
        }
    }
    
    public async Task<Result<UserInfoDto>> GetUserInfo(Guid userId)
    {
        try
        {
            var data = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserInfoDto(u.Id, u.UserName, u.Name, u.Email))
                .FirstOrDefaultAsync();

            return data is null 
                ? Result<UserInfoDto>.Failure("No se pudo obtener la información del usuario", 404) 
                : Result<UserInfoDto>.Success(data);
        }
        catch (Exception e)
        {
            return Result<UserInfoDto>.Failure("No se pudo obtener la información del usuario");
        }
    }
}