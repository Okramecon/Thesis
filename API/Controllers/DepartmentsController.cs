using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.DepartmentModels;

namespace API.Controllers
{
    public class DepartmentsController : BaseController
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetDepartmentModel>> List()
        {
            return await _departmentService.List<GetDepartmentModel>();
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
