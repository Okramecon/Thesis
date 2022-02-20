using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Model.Models.UserStoryModels;

namespace API.Controllers
{
    public class UserStoriesController : BaseController
    {

        private readonly UserStoryService _userStoryService;

        public UserStoriesController(UserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetUserStoryModel>> List()
        {
            return await _userStoryService.List<GetUserStoryModel>();
        }

        [HttpGet("{id}")]
        public async Task<GetUserStoryModel> Get(int id)
        {
            return await _userStoryService.ById<GetUserStoryModel>(id);
        }

        [HttpPost]
        public async Task<int> Post(AddUserStoryModel model)
        {
            return (await _userStoryService.Add(model)).Id;
        }

        [HttpPut]
        public async Task Edit(EditUserStoryModel model)
        {
            await _userStoryService.Edit(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userStoryService.Delete(id);
        }
    }
}
