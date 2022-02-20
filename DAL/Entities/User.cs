using System.Collections.Generic;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class User : IdentityUser, IIdHasInt<string>
    {
        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
