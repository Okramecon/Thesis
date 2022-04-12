using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.CommentModels;

namespace API.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetCommentModel>> List()
        {
            return await _commentService.List<GetCommentModel>();
        }

        [HttpGet("{id}")]
        public async Task<GetCommentModel> Get(int id)
        {
            return await _commentService.ById<GetCommentModel>(id);
        }

        [HttpPost]
        public async Task<int> Post(AddCommentModel model)
        {
            return (await _commentService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(GetCommentModel model)
        {
            await _commentService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _commentService.Delete(id);
        }
    }
}
