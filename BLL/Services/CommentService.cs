using BLL.Services.Bases;
using Common.Exceptions;
using DAL.EF;
using DAL.Entities;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Models.CommentModels;

namespace BLL.Services
{
    public class CommentService : EntityService<Comment, int>
    {
        private MediaService MediaService { get; }

        public CommentService(AppDbContext context, MediaService mediaService) : base(context, context.Comments)
        {
            MediaService = mediaService;
        }

        protected override async Task BeforeAdd(Comment entity)
        {
            entity.SendTime = System.DateTime.UtcNow;
        }

        public async Task<IEnumerable<GetCommentModel>> GetTicketCommentsAsync(int ticketId)
        {
            return await Entities
                .Where(c => c.TicketId == ticketId)
                .OrderBy(c => c.SendTime)
                .ProjectToType<GetCommentModel>()
                .ToListAsync();
        }

        public async Task Delete(int id, string userId)
        {
            var entity = await Entities
                .Include(x => x.User)
                .ById(id);

            if(entity.User.Id != userId)
            {
                throw new InnerException("You can delete only your comments!", "9fe608d9-9c9a-4491-834c-1031909fd96b");
            }

            Entities.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<int> Add(AddCommentModel model)
        {
            var entity = model.Adapt<Comment>();
            entity.Attachments = null;
            await BeforeAdd(entity);
            Context.Comments.Add(entity);
            await Context.SaveChangesAsync();

            entity.Attachments = Context.Medias.Where(x => model.Attachments.Contains(x.Id)).ToList();
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<GetCommentModel> GetById(int id)
        {
            var result = await base.ById<GetCommentModel>(id);
            result.Attachments = await MediaService.GetList(result.Attachments.Select(x => x.Id).ToList());

            return result;
        }
    }
}
