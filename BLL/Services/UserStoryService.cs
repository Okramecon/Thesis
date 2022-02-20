using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class UserStoryService : EntityService<UserStory, int>
    {
        public UserStoryService(AppDbContext context) : base(context, context.UserStories) { }

    }
}
