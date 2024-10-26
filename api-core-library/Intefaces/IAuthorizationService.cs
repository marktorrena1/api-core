using System;
using api_core_library.Models;

namespace api_core_library.Intefaces;

public interface IAuthorizationService
{
    Task<string> Authenticate(string username, string password);
}
