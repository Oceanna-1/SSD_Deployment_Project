using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace SSD_Lab2.Data;

using System.ComponentModel.DataAnnotations;
public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
    public string? City { get; set; } = "";
}
