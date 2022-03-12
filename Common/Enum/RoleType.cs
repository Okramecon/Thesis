using System.ComponentModel;

namespace Common.Enums
{
    public enum RoleType : byte
    {
        /// <summary>
        /// Admin
        /// </summary>
        [Description("Admin")]
        Admin = 1,

        /// <summary>
        /// User
        /// </summary>
        [Description("User")]
        User = 2
    }
}