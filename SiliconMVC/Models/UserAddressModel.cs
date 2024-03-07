using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models;

public class UserAddressModel
{
    [Display(Name = "Address line 1", Order = 5)]
    [DataType(DataType.Text)]
    public string? AddressLine1 { get; set; }

    [Display(Name = "Address line 2", Order = 6)]
    [DataType(DataType.Text)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Postal code", Order = 7)]
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    [Display(Name = "City", Order = 8)]
    [DataType(DataType.Text)]
    public string? City { get; set; }
}
