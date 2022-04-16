using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.BoardModels;

namespace API.Controllers
{
    public class BoardsController : BaseController
    {

        private readonly BoardService _boardService;
        public BoardsController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetBoardModel>> List()
        {
            return await _boardService.List<GetBoardModel>();
        }

        [HttpGet("{id}")]
        public async Task<GetBoardModel> Get(int id)
        {
            return await _boardService.ById<GetBoardModel>(id);
        }

        [HttpPost]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task<int> Post(AddBoardModel model)
        {
            return await _boardService.Add(model);
        }

        [HttpPut]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task Edit(EditBoardModel model)
        {
            await _boardService.Edit(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin)]
        public async Task Delete(int id)
        {
            await _boardService.Delete(id);
        }

        [HttpGet("byProjectId")]
        public async Task<BoardModels.GetBoardModel> GetByProjectId(int projectId)
        {
            return await _boardService.GetByProjectId<BoardModels.GetBoardModel>(projectId);
        }
    }
}
