using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.CommentModels;
using static Model.Models.TicketModels;

namespace API.Controllers
{
    public class TicketsController : BaseController
    {
        private readonly TicketService _ticketService;
        private readonly CommentService _commentService;

        public TicketsController(TicketService ticketService, CommentService commentService)
        {
            _ticketService = ticketService;
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<GetTicketModel> Get(int id)
        {
            return await _ticketService.GetById(id);
        }

        [HttpGet("{id}/comments")]
        public async Task<IEnumerable<GetCommentModel>> GetComments(int id)
        {
            return await _commentService.GetTicketCommentsAsync(id);
        }

        [HttpPost]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<int> Post(AddTicketModel model)
        {
            return await _ticketService.Add(model);
        }

        [HttpPut]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
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
