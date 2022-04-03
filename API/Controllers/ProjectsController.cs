using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.ProjectModels;
using static Model.Models.TicketModels;

namespace API.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly ProjectService _projectService;
        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetProjectModel>> List()
        {
            return await _projectService.List<GetProjectModel>();
        }

        [HttpGet("{id}")]
        public async Task<GetProjectModel> Get(int id)
        {
            return await _projectService.ById<GetProjectModel>(id);
        }

        [HttpGet("{id}/tickets")]
        public async Task<IEnumerable<GetTicketModel>> GetTickets(int id)
        {
            return await _projectService.GetTicketsByProjectIdAsync(id);
        }

        [HttpPost]
        public async Task<int> Post(AddProjectModel model)
        {
            return (await _projectService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditProjectModel model)
        {
            await _projectService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectService.Delete(id);
        }
    }
}
