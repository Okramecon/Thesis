using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;
using Mapster;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TicketService : EntityService<Ticket, int>
    {
        private ProjectService ProjectService { get; }

        private MediaService MediaService { get; }

        public TicketService(AppDbContext context, ProjectService projectService, MediaService mediaService) : base(context, context.Tickets)
        {
            ProjectService = projectService;
            MediaService = mediaService;
        }

        protected override Task BeforeAdd(Ticket ticket)
        {
            ticket.CreatedDatetime = System.DateTime.Now;
            return Task.CompletedTask;
        }

        public async Task<int> Add(TicketModels.AddTicketModel model)
        {
            var entity = model.Adapt<Ticket>();
            entity.Attachments = null;
            await BeforeAdd(entity);
            Context.Tickets.Add(entity);
            await Context.SaveChangesAsync();
            
            entity.Attachments = Context.Medias.Where(x => model.Attachments.Contains(x.Id)).ToList();
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<TicketModels.GetTicketModel> GetById(int id)
        {
            var result = await base.ById<TicketModels.GetTicketModel>(id);
            result.Attachments = await MediaService.GetList(result.Attachments.Select(x => x.Id).ToList());

            return result;
        }

        public async Task<> List()
        {

        }
    }
}
