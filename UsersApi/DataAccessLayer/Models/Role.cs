using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.DataAccessLayer.Models;

[Table("roles")]
public class Role
{
    [Key]
    [Column("role_id")]
    [Required] 
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [NotMapped] 
    public string Name => RoleType.ToString();

    [Column("role_type")]
    [Required] 
    public RoleType RoleType { get; set; } = RoleType.User;
}   
