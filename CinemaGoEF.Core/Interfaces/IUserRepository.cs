using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface IUserRepository
{
    Task<ICollection<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
}