using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class DepartmentService : EntityService<Department, int>
    {
        public DepartmentService(AppDbContext context) : base(context, context.Departments) { }

    }
}
