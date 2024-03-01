using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models;

public class SignUpModel
{
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Required entry")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Required entry")]
    public string LastName { get; set; } = null!;

    [EmailAddress]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [RegularExpression("[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+", ErrorMessage = "Not a valid email address")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password", Order = 4)]
    [Compare(nameof(Password), ErrorMessage = "Password and password confirmation not valid")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "I agree to the Terms & Conditions.", Order = 5)]
    [Required(ErrorMessage = "You must agree to Terms and Conditions")]
    public bool TermsAndConditions = false;
}
