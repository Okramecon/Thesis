using System.ComponentModel;

namespace Common.Enums
{
    public enum RoleType : byte
    {
        /// <summary>
        /// Admin
        /// </summary>
        [Description("admin")]
        Admin = 1,

        /// <summary>
        /// User
        /// </summary>
        [Description("user")]
        User = 2,

        /// <summary>
        /// DepartmentAdmin
        /// </summary>
        [Description("departmentAdmin")]
        DepartmentAdmin = 3
    }
}