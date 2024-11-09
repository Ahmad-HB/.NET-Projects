using System.ComponentModel.DataAnnotations;
using Domain.Resource;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.ViewModel;

public class RolesViewModel
{
    public List<IdentityRole> Roles { get; set; }
    public NewRole NewRole { get; set; }
}

public class NewRole
{
    public Guid RoleId { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RoleName")]
    public string RoleName { get; set; }
}