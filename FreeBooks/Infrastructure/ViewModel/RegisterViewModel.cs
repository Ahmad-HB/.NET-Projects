using System.ComponentModel.DataAnnotations;
using Domain.Entity;
using Domain.Resource;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.ViewModel;

public class RegisterViewModel
{
    public List<VwUser> Users { get; set; }
    public NewRegister NewRegister { get; set; }
    public List<IdentityRole> Roles { get; set; }
    public ChangePasswordViewModel ChangePassword { get; set; }
}

public class NewRegister
{
    public Guid Id { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterName")]
    [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
    public string Name { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RoleName")]
    public string RoleName { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterEmail")]
    [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterEmailError")]
    public string Email { get; set; }
    public string ImageUser { get; set; }
    public bool ActiveUser { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Password")]
    [MaxLength(15, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(8, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLengthPassword")]
    public string Password { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ComparePassword")]
    [Compare("Password",ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ComparePasswordError")]
    public string ComparePassword { get; set; }
}