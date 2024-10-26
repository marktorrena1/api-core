using System;
using api_core_library.Intefaces;
using api_core_library.Models;
using api_core_library.Repositories;
using Microsoft.Data.SqlClient;

namespace api_core_library.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthorizationService( IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var acc = await _userRepository.GetUserByUsernameAsync(username);
        if (acc == null || !BCrypt.Net.BCrypt.Verify(password, acc.PasswordHash))
        {
            return null;
        }
        return _jwtService.GenerateToken(acc);
    }
}
