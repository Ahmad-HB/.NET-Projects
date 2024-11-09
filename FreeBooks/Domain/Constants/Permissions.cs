using Domain.Entity;

namespace Domain.Constants;

public static class Permissions
{
    public static List<string> GeneratePermissionsFromModule(string module)
    {
        return new List<string>
        {
            $"Permissions.{module}.View",
            $"Permissions.{module}.Create",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static List<string> PermissionList()
    {
        var allPermissions = new List<string>();

        foreach (var module in Enum.GetValues(typeof(Helper.PermissionModuleName)))
        {
            allPermissions.AddRange(GeneratePermissionsFromModule(module.ToString()));
        }
        return allPermissions;
    }
    
    public static class Home
    {
        public const string View = "Permissions.Home.View";
        public const string Create = "Permissions.Home.Create";
        public const string Edit = "Permissions.Home.Edit";
        public const string Delete = "Permissions.Home.Delete";
    }
    public static class Accounts
    {
        public const string View = "Permissions.Accounts.View";
        public const string Create = "Permissions.Accounts.Create";
        public const string Edit = "Permissions.Accounts.Edit";
        public const string Delete = "Permissions.Accounts.Delete";
    }
    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
    }
    public static class Registers
    {
        public const string View = "Permissions.Registers.View";
        public const string Create = "Permissions.Registers.Create";
        public const string Edit = "Permissions.Registers.Edit";
        public const string Delete = "Permissions.Registers.Delete";
    }
    public static class Categories
    {
        public const string View = "Permissions.Categories.View";
        public const string Create = "Permissions.Categories.Create";
        public const string Edit = "Permissions.Categories.Edit";
        public const string Delete = "Permissions.Categories.Delete";
    }
}
