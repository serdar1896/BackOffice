using System.Collections.Generic;

namespace Inveon.Core.Models.Enum
{
    public class EnumRole
    {
        public static RoleModel User => new RoleModel { Id = 0, Text = "User" };
        public static RoleModel Admin => new RoleModel { Id = 1, Text = "Admin" };
        public static RoleModel Guest => new RoleModel { Id = 2, Text = "Guest" };
        public static List<RoleModel> RoleModelList => new List<RoleModel>() { User, Admin, Guest };

    }
    public class RoleModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public enum Roles
    {
        User = 0,
        Admin = 1,
        Guest = 2
    }

}


