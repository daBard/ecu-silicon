using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class UserEntity
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "nvarchar(20)")]
    public string? Phone { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Bio { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime RegistrationDate { get; set; }

    [ForeignKey(nameof(Auth))]
    public int AuthId { get; set; }

    [ForeignKey(nameof(Address))]
    public int? AddressId { get; set; }

    public virtual AuthEntity Auth { get; set; } = null!;

    public virtual AddressEntity? Address { get; set; }
}
