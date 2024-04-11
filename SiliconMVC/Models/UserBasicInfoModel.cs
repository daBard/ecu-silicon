using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models;

public class UserBasicInfoModel
{
    [Display(Name = "First name", Order = 0)]
    [Required(ErrorMessage = "Enter a valid first name")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Order = 1)]
    [Required(ErrorMessage = "Enter a valid last name")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email address", Order = 2)]
    [Required(ErrorMessage = "Enter a valid email address")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
    [DataType(DataType.MultilineText)]
    public string? Bio { get; set; }
}
