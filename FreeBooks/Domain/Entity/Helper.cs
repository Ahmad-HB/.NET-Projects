namespace Domain.Entity;

public class Helper
{
    public const string PathImageUser = "/Images/Users/";
    public const string PathSaveImageUser = "Images/Users/";
    
    public const string Success = "success";
    public const string Error = "error";

    public const string MsgType = "msgType";
    public const string Title = "title";
    public const string Msg = "msg";

    public const string Save = "Save";
    public const string Delete = "Delete";
    public const string Update = "Update";
    
    
    public const string Email = "superadmin@domin.com";
    public const string UserName = "superadmin@domin.com";
    public const string Name = "SuperAdmin";
    public const string Password = "superadmin@P@$$w0rd123456";

    public const string EmailBasic = "basicuser@domin.com";
    public const string UserNameBasic = "basicuser@domin.com";
    public const string NameBasic = "BasicUser";
    public const string PasswordBasic = "basicuser@P@$$w0rd123456";

    public const string Permission = "Permission";
    
    
    public enum eCurrentState 
    {
        Active = 1,
        Deleted = 0,
    } 
    
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Basic
    }

    public enum PermissionModuleName
    {
        Home,
        Accounts,
        Roles,
        Registers,
        Categories
    }
}