using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.TicketModels;

namespace API.Controllers
{
    public class TicketsController : BaseController
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTicketModel>> List()
        {
            return await _ticketService.List<GetTicketModel>();
        }

        [HttpGet("{id}")]
        public async Task<GetTicketModel> Get(int id)
        {
            return await _ticketService.ById<GetTicketModel>(id);
        }

        [HttpPost]
        public async Task<int> Post(AddTicketModel model)
        {
            return (await _ticketService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditTicketModel model)
        {
            await _ticketService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _ticketService.Delete(id);
        }
    }
}
