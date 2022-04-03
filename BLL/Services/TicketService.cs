using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class TicketService : EntityService<Ticket, int>
    {
        private readonly ProjectService _projectService;

        public TicketService(AppDbContext context, ProjectService projectService) : base(context, context.Tickets)
        {
            _projectService = projectService;
        }
    }
}
