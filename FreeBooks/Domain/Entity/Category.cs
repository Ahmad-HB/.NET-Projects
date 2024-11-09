using System.ComponentModel.DataAnnotations;
using Domain.Resource;

namespace Domain.Entity;

public class Category
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "CtaegoryName")]
    [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
    public string Name { get; set; }
    public string Description { get; set; }
    
    
    public int CurrentState { get; set; }
    
}