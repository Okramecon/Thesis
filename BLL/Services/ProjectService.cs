using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class ProjectService : EntityService<Project, int>
    {
        public ProjectService(AppDbContext context) : base(context, context.Projects) { }

    }
}
