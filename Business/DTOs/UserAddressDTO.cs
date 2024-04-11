namespace Business.DTOs;

public class UserAddressDTO
{   
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

}
