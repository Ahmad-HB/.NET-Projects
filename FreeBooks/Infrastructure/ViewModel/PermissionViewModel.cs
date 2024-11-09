namespace Infrastructure.ViewModel;

public class PermissionViewModel
{
    public string RoleId { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;

    public List<RoleClaimViewModel> RoleClaims { get; set; } = new List<RoleClaimViewModel>();
}

public class RoleClaimViewModel
{
    public string value { get; set; } =string.Empty;
    public bool selected { get; set; }
}