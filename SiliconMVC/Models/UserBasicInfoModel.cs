using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models;

public class UserBasicInfoModel
{
    [Display(Name = "First name", Order = 0)]
    [DataType(DataType.Text)]
    public string? FirstName { get; set; }

    [Display(Name = "Last name", Order = 1)]
    [DataType(DataType.Text)]
    public string? LastName { get; set; }

    [Display(Name = "Email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email address is required")]
    public string? Email { get; set; }

    [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
    [DataType(DataType.MultilineText)]
    public string? Bio { get; set; }
}
