using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.DepartmentModels;
using static Model.Models.ProjectModels;

namespace API.Controllers
{
    public class DepartmentsController : BaseController
    {
        private DepartmentService DepartmentService { get; }
        private ProjectService ProjectService { get; }
        private CurrentUserService CurrentUserService { get; }

        public DepartmentsController(DepartmentService departmentService, ProjectService projectService, CurrentUserService currentUserService)
        {
            DepartmentService = departmentService;
            ProjectService = projectService;
            CurrentUserService = currentUserService;
        }

        [HttpGet]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<IEnumerable<GetDepartmentModel>> List()
        {
            var (userId, roles) = (CurrentUserService.GetCurrentUserId(), await CurrentUserService.GetRoles());
            return await DepartmentService.ListByUser<GetDepartmentModel>(userId, roles);
        }

        [HttpGet]
        [Route("{id}/projects")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<IEnumerable<GetProjectModel>> ProjectsByDepartment(int id)
        {
            return await ProjectService.GetProjectsByDepartmentId(id);
        }

        [HttpGet("{id}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<GetDepartmentModel> Get(int id)
        {
            var (userId, roles) = (CurrentUserService.GetCurrentUserId(), await CurrentUserService.GetRoles());
            
            return await DepartmentService.ById<GetDepartmentModel>(id, userId, roles);
        }

        [HttpPost]
        [AuthorizeRoles(RoleType.Admin)]
        public async Task<int> Post(AddDepartmentModel model)
        {
            return await DepartmentService.Add(model);
        }

        [HttpPut]
        [AuthorizeRoles(RoleType.Admin)]
        public async Task Edit(EditDepartmentModel model)
        {
            await DepartmentService.Edit(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(RoleType.Admin)]
        public async Task Delete(int id)
        {
            await DepartmentService.Delete(id);
        }

        [HttpGet("addUser/{id}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task AddUserToDepartment(string userId, int id)
        {
            await DepartmentService.AddUserToDepartment(userId, id);
        }

        [HttpGet("addUserByEmail/{id}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task AddUserToDepartmentByEmail(string userName, int id)
        {
            await DepartmentService.AddUserToDepartmentByEmail(userName, id);
        }

        [HttpGet("{id}/users")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task<List<UserModels.ByIdOut>> GetDepartmentUsers(int id)
        {
            return await DepartmentService.GetUsersByDepartment<UserModels.ByIdOut>(id);
        }

        [HttpDelete("{id}/user/{userIdToDelete}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task RemoveUserFromDepartment(int id, string userIdToDelete)
        {
            var (userId, _) = (CurrentUserService.GetCurrentUserId(), await CurrentUserService.GetRoles());
            await DepartmentService.RemoveUserFromDepartment(id, userIdToDelete, userId);
        }
    }
}
