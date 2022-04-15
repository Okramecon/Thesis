using System.Collections.Generic;
using System.Threading.Tasks;
using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/News")]
    public class NewsController
    {
        private NewsService NewsService { get; set; }

        private CurrentUserService CurrentUserService { get; set; }

        public NewsController(NewsService newsService, CurrentUserService currentUserService)
        {
            NewsService = newsService;
            CurrentUserService = currentUserService;
        }

        [HttpGet]
        [Route("byDepartmentId")]
        public async Task<List<NewsModels.ById>> ById(int departmentId)
        {
            return await NewsService.GetByDepartmentId(departmentId);
        }

        [HttpPost]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task<int> Add(NewsModels.Add model)
        {
            return await NewsService.Add(model, CurrentUserService.GetCurrentUserId());
        }
    }
}
