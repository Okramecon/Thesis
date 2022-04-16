using System.Linq;
using System.Threading.Tasks;
using BLL.Services.Bases;
using Common.Exceptions;
using Common.Extensions;
using DAL.EF;
using DAL.Entities;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class BoardService : EntityService<Board, int>
    {
        public BoardService(AppDbContext context) : base(context, context.Boards) { }

        public async Task<T> GetByProjectId<T>(int projectId)
        {
            var entity = (await Context.Projects
                .Include(x => x.Boards)
                .ById(projectId))
                .Boards
                .FirstOrDefault();
            if(entity.IsNull())
            {
                throw new InnerException("Project has no boards!", "f38a1666-1028-42fe-8798-1746bbb9e434");
            }

            return entity.Adapt<T>();
        }
    }
}
