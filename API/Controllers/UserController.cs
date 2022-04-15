using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        private UserService Service { get; }

        private CurrentUserService CurrentUserService { get; }

        public UsersController(UserService userService, CurrentUserService currentUserService)
        {
            Service = userService;
            CurrentUserService = currentUserService;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<string> Register(UserModels.AddIn model)
        {
            return await Service.RegisterAsync(model);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<UserModels.ByIdOut> ById(string id)
        {
            return await Service.GetById<UserModels.ByIdOut>(id);
        }

        // todo add me endpoint, extracting user id from bearer token, replace byId endpoint
        // replace edit by id with edit me

        /// <summary>
        /// Edits user
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [AuthorizeRoles(RoleType.User)]
        public async Task EditUser(string id, UserModels.EditIn model)
        {
            if(CurrentUserService.GetCurrentUserId() != id)
            {
                throw new InnerException("Cannot edit another user!", "76d9eea7-ca9a-4ecb-be75-7f38b273d7ab");
            }

            await Service.Edit(id, model);
        }
    }
}