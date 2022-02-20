using BLL.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<int> Post(AddBoardModel model)
        {
            return (await _boardService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditBoardModel model)
        {
            await _boardService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _boardService.Delete(id);
        }
    }
}
