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
        private DepartmentService DepartmentService { get; }
        private ProjectService ProjectService { get; }

        public DepartmentsController(DepartmentService departmentService, ProjectService projectService)
        {
            DepartmentService = departmentService;
            ProjectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetDepartmentModel>> List()
        {
            return await DepartmentService.List<GetDepartmentModel>();
        }

        [HttpGet]
        [Route("{id}/projects")]
        public async Task<IEnumerable<GetProjectModel>> ProjectsByDepartment(int id)
        {
            return await ProjectService.GetProjectsByDepartmentId(id);
        }

        [HttpGet("{id}")]
        public async Task<GetDepartmentModel> Get(int id)
        {
            return await DepartmentService.ById<GetDepartmentModel>(id);
        }

        [HttpPost]
        public async Task<int> Post(AddDepartmentModel model)
        {
            return (await DepartmentService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditDepartmentModel model)
        {
            await DepartmentService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await DepartmentService.Delete(id);
        }
    }
}
