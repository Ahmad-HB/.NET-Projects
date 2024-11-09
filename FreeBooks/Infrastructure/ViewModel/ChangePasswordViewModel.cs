using System.ComponentModel.DataAnnotations;
using Domain.Resource;

namespace Infrastructure.ViewModel;

public class ChangePasswordViewModel
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources),ErrorMessageResourceName = "Password")]
    [MinLength(8, ErrorMessageResourceType = typeof(Resources),ErrorMessageResourceName = "MinLengthPassword")]
    public string NewPassword { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources),ErrorMessageResourceName = "ComparePassword")]
    [Compare("NewPassword")]
    public string ComparePassword { get; set; }
    
    
    
}