using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API.Infrastructure
{
    public class CurrentUserService
    {
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
