using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Resource;
using Domain.Resource;

namespace Domain.Entity;

public class SubCategory
{
    public Guid Id { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "SubCategoryName")]
    [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}