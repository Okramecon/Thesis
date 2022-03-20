using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        private UserService Service { get; }

        public UsersController(UserService userService)
        {
            Service = userService;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<string> Register(UserModel.AddIn model)
        {
            return await Service.RegisterAsync(model);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<UserModel.ByIdOut> ById(string id)
        {
            return await Service.GetById<UserModel.ByIdOut>(id);
        }

        // todo add me endpoint, extracting user id from bearer token, replace byId endpoint
        // replace edit by id with edit me

        /// <summary>
        /// Edits user
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task EditUser(string id, UserModel.EditIn model)
        {
            await Service.Edit(id, model);
        }
    }
}