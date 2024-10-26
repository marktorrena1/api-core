using System;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace api_core_library.Models;

public class Company
{
    public int Id { get; set; }
    public required string CompanyName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
