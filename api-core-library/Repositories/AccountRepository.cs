using System;
using api_core_library.Intefaces;
using api_core_library.Models;

namespace api_core_library.Repositories;

public class AccountRepository : IAccountRepository
{
    // temporary storage in memory
    private readonly List<Account> _accounts = new()
    {
        new Account { Username = "admin", Password = "admin123", Role = "Admin", UserId = 1 },
        new Account { Username = "user", Password = "user123", Role = "User" , UserId = 2}
    };

    public Account GetUser(string username)
    {
        return _accounts.FirstOrDefault(a => a.Username == username);
    }
}
