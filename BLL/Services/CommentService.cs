using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Models.CommentModels;

namespace BLL.Services
{
    public class CommentService : EntityService<Comment, int>
    {
        public CommentService(AppDbContext context) : base(context, context.Comments) { }

        protected override async Task BeforeAdd(Comment entity)
        {
            entity.SendTime = System.DateTime.UtcNow;
            entity.UserId = (await Context.Users.FirstOrDefaultAsync()).Id;
        }

        public async Task<IEnumerable<GetCommentModel>> GetTicketCommentsAsync(int ticketId)
        {
            return await Entities
                .Where(c => c.TicketId == ticketId)
                .OrderBy(c => c.SendTime)
                .ProjectToType<GetCommentModel>()
                .ToListAsync();
        }
    }
}
