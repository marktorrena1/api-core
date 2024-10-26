using System;
using api_core_library.Intefaces;

namespace api_core_library.Models;

public class UserDto : User, IRoles
{
    public required string RoleName { get; set; }
}
