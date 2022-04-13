using Common.Interfaces;
using System;
using System.Collections.Generic;
using static Model.Models.UserStoryModels;

namespace Model.Models
{
    public static class BoardModels
    {
        public class GetBoardModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public int ProjectId { get; set; }
        }

        public class GetDetailBoardModel : GetBoardModel
        {
            public ICollection<GetDetailUserStoryModel> UserStories { get; set; }
        }

        public class AddBoardModel
        {
            public string Title { get; set; }
            public int ProjectId { get; set; }
        }

        public class EditBoardModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}
