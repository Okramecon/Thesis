using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure
{
    public class CurrentUserService
    {
        private IHttpContextAccessor HttpContextAccessor { get; }

        private UserManager<User> UserManager { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            HttpContextAccessor = httpContextAccessor;
            UserManager = userManager;
        }

        public string GetCurrentUserId()
        {
            var userId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId.IsNull())
            {
                throw new InnerException("Not logged in!", "ce5741e1-fded-48f9-8450-3b18dd662543");
            }

            return userId;
        }

        public async Task<ICollection<RoleType>> GetRoles()
        {
            var userId = GetCurrentUserId();
            var roles = await UserManager.GetRolesAsync(new User
            {
                Id = userId
            });

            return roles.Select(x => Enum.Parse<RoleType>(x)).ToList();
        }
    }
}
