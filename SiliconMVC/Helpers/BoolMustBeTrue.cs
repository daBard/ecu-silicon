using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Helpers;

public class BoolMustBeTrue : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool b && b;
    }
}
