using System;
using api_core_library.Intefaces;
using api_core_library.Models;

namespace api_core_library.Services;

public class UserService: IUserService
{
 private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public UserService( IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    async Task<IEnumerable<UserDto>> IUserService.GetUser(UserDto account)
    {
      // if user is admin show all users based on assigned companyId
      // if user is non admin/user show users with same companyId and non admin
      if (account.RoleName == "Admin") account.RoleName = null;
      var acc = await _userRepository.GetUserAsync(account);
      return acc;
    }
}
