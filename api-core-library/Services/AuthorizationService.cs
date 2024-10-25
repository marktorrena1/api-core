using System;
using api_core_library.Intefaces;
using api_core_library.Models;

namespace api_core_library.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly JwtService _jwtService;

    public AuthorizationService(IAccountRepository accountRepository, JwtService jwtService)
    {
        _accountRepository = accountRepository;
        _jwtService = jwtService;
    }

    public string Authenticate(Account loginRequest)
    {
        var acc = _accountRepository.GetUser(loginRequest.Username);
        // temporary add hash checking
        if (acc == null || acc.Password != loginRequest.Password)
        {
            return null;
        }

        return _jwtService.GenerateToken(acc.Username, acc.Role, 7);
    }
}
