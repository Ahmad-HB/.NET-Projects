using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Resource;
using Domain.Resource;

namespace Domain.Entity;

public class Book
{
    public Guid Id { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "BookName")]
    [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
    public string Name { get; set; }
    [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "AuthorName")]
    [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxLength")]
    [MinLength(3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MinLength")]
    public string Author { get; set; }
    
    public string ImageName { get; set; }
    public string FileName { get; set; }
    
    public string Description { get; set; }
    
    public bool Publish { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public Guid SubCategoryId { get; set; }
    [ForeignKey("SubCategoryId")]
    public SubCategory SubCategory { get; set; }

    public int CurrentState { get; set; }
}