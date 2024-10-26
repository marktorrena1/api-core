using System;
using api_core_library.Models;

namespace api_core_library.Intefaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUser(UserDto user);
}
