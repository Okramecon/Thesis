using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.DepartmentModels;
using static Model.Models.ProjectModels;

namespace API.Controllers
{
    public class DepartmentsController : BaseController
    {
        private readonly DepartmentService _departmentService;
        private readonly ProjectService _projectService;

        public DepartmentsController(DepartmentService departmentService, ProjectService projectService)
        {
            _departmentService = departmentService;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetDepartmentModel>> List()
        {
            return await _departmentService.List<GetDepartmentModel>();
        }

        [HttpGet]
        [Route("{id}/projects")]
        public async Task<IEnumerable<GetProjectModel>> ProjectList(int id)
        {
            return await _projectService.GetProjectsByDepartmentId(id);
        }

        [HttpGet("{id}")]
        public async Task<GetDepartmentModel> Get(int id)
        {
            return await _departmentService.ById<GetDepartmentModel>(id);
        }

        [HttpPost]
        public async Task<int> Post(AddDepartmentModel model)
        {
            return (await _departmentService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditDepartmentModel model)
        {
            await _departmentService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _departmentService.Delete(id);
        }
    }
}
