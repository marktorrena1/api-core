using api_core_library.Intefaces;
using Microsoft.Extensions.Logging;

namespace api_core_library.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly ILogger<AuthorizationService> _logger;

    public AuthorizationService( IUserRepository userRepository, JwtService jwtService, ILogger<AuthorizationService> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        try
        {
            var acc = await _userRepository.GetUserByUsernameAsync(username);
            if (acc == null || !BCrypt.Net.BCrypt.Verify(password, acc.PasswordHash))
            {
                _logger.LogWarning("Invalid login attempt: {Username}", username);
                return null;
            }
            return _jwtService.GenerateToken(acc);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in Authenticate: {Username}", username);
            throw new ApplicationException("An error occurred during authentication.");
        }
    }
}
