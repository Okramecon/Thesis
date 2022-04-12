
using System.Threading.Tasks;
using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace API.Controllers
{
    public class NewsController : BaseController
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
        public async Task<NewsModels.ByIdOut> GetById(int id)
        {
            return await NewsService.GetById(id);
        }

        [HttpPost]
        [Route("")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task<int> Add(NewsModels.AddIn model)
        {
            return await NewsService.Add(model, CurrentUserService.GetCurrentUserId());
        }
    }
}
