using System.ComponentModel.DataAnnotations;
using Domain.Resource;

namespace Infrastructure.ViewModel;

public class LoginViewModel
{
    
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RegisterEmail")]
    public string Email { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Password")]
    public string Password { get; set; }
    
    
    public bool RememberMe { get; set; }
}