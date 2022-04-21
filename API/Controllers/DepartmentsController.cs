﻿using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
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
    }
}
