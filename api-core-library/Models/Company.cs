using System;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace api_core_library.Models;

public class Company
{
    public Guid Id { get; set; }
    public int CompanyId { get; set; }
    public required string CompanyName { get; set; }
}
