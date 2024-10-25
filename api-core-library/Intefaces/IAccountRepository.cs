using System;
using api_core_library.Models;

namespace api_core_library.Intefaces;

public interface IAccountRepository
{
    Account GetUser(string username);
}
