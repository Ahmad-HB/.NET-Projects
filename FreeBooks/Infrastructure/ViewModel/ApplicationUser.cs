using Microsoft.AspNetCore.Identity;

namespace Infrastructure.ViewModel;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string ImageUser { get; set; }
    public bool ActiveUser { get; set; }
    
}