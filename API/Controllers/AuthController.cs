using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        private AuthService AuthService { get; set; }

        private RoleManager<Role> RoleManager { get; set; }

        public AuthController(AuthService authService, RoleManager<Role> roleManager)
        {
            AuthService = authService;
            RoleManager = roleManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<AuthModel.Response> Login(AuthModel.Login model)
        {
            return await AuthService.AccessToken(model);
        }

        [HttpPost]
        [Route("Role")]
        public async Task AddRole(string roleName)
        {
            await RoleManager.CreateAsync(new Role()
            {
                Name = roleName
            });
        }

        [HttpGet]
        [Route("Roles")]
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await RoleManager.Roles.ToListAsync();
        }
    }
}