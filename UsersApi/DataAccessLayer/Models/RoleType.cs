using System.ComponentModel.DataAnnotations;

namespace UsersApi.DataAccessLayer.Models;

public enum RoleType
{
    [Display(Name = "Administrator")]
    Administrator,
    [Display(Name = "User")]
    User
}