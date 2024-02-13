using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.DataAccessLayer.Models;

[Table("users")]
public class User
{
    [Key] 
    [Column("user_id")] 
    [Required] 
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("email")]
    [DataType("varchar(255)")]
    [Required] 
    public string Email { get; set; } = string.Empty;

    [Column("password")]
    [Required] 
    public byte[] Password { get; set; } = [];

    [Column("salt")]
    [Required] 
    public byte[] Salt { get; set; } = [];
    
    [Column("role_id")]
    [Required] 
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    [NotMapped]
    public Role? Role { get; set; } = null;
}
