using System;

namespace api_core_library.Models;

public class Account
{
    public Guid Id { get; set; }
    public required string Username { get; set; }    
    public required string Password { get; set; }
    public string? Role { get; set; }     
    public int CompanyId { get; set; }
    public int UserId { get; set; }

}
