using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AuthEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Password { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string SecurityKey { get; set; } = null!;

    public virtual UserEntity User { get; set; } = null!;
}
