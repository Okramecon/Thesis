using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : EntityService<Comment, int>
    {
        public CommentService(AppDbContext context) : base(context, context.Comments) { }

        protected override Task BeforeAdd(Comment entity)
        {
            entity.SendTime = System.DateTime.UtcNow;
            return Task.CompletedTask;
        }

    }
}
