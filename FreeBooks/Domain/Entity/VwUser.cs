namespace Domain.Entity;

public class VwUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImageUser { get; set; }
    public bool ActiveUser { get; set; }
    public string Role { get; set; }
}