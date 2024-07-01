using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Image { get; set; }
    public string? Role { get; set; }
}