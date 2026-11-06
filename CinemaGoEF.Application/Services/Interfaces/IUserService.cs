using CinemaGoEF.Core.Models.DTOs.UserDTOs;

namespace CinemaGoEF.Application.Services.Interfaces;

public interface IUserService
{
    Task Register(CreateUserDto userDto);
    Task VerifyRegistration(string email, string code);
    Task<UserDto?> Login(string email, string password);
    Task<UserDto?> GetUserById(int id);
    Task<ICollection<UserDto>> GetAllUsers();
    Task MakeAdmin(int userId);
    Task RemoveAdmin(int userId);
    Task AddBalance(int userId, decimal amount);
    Task<decimal> GetBalance(int userId);
    Task UpdateUser(int id, UpdateUserDto userDto);
}