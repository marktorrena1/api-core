using System;
namespace api_core_library.Models;

public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int CompanyId { get; set; }
    }
