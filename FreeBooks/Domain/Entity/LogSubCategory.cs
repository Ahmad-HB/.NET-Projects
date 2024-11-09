using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class LogSubCategory
{
    public Guid Id { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }

    public Guid SubCategoryId { get; set; }
    [ForeignKey("SubCategoryId")]
    public Category SubCategory { get; set; }
}