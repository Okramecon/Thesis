using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class TicketService : EntityService<Ticket, int>
    {
        public TicketService(AppDbContext context) : base(context, context.Tickets) { }

    }
}
