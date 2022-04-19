using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.CommentModels;

namespace API.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly CommentService _commentService;

        private CurrentUserService CurrentUserService { get; }

        public CommentsController(CommentService commentService, CurrentUserService currentUserService)
        {
            _commentService = commentService;
            CurrentUserService = currentUserService;
        }

        [HttpGet("{id}")]
        public async Task<GetCommentModel> Get(int id)
        {
            return await _commentService.GetById(id);
        }

        [HttpPost]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<int> Post(AddCommentModel model)
        {
            var userId = CurrentUserService.GetCurrentUserId();
            model.UserId = userId;
            return await _commentService.Add(model);
        }

        [HttpPut]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task Edit(GetCommentModel model)
        {
            var userId = CurrentUserService.GetCurrentUserId();
            if (model.UserId != userId)
            {
                throw new InnerException("You can edit only your comment", "111c1213-3b40-442e-af94-9f5f7060ca28");
            }

            await _commentService.Edit(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task Delete(int id)
        {
            var userId = CurrentUserService.GetCurrentUserId();

            await _commentService.Delete(id, userId);
        }
    }
}
