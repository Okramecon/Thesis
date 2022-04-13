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
        [Route("{id}")]
        public async Task<NewsModels.ById> ById(int id)
        {
            return await NewsService.GetById(id);
        }

        [HttpPost]
        //[AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<int> Add(NewsModels.Add model)
        {
            return await NewsService.Add(model, CurrentUserService.GetCurrentUserId());
        }
    }
}
