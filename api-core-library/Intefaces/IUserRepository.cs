using System;
using api_core_library.Models;

namespace api_core_library.Intefaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<IEnumerable<UserDto>> GetUserAsync(UserDto user);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}
