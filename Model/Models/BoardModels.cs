using Common.Interfaces;
using System;

namespace Model.Models
{
    public static class BoardModels
    {
        public class GetBoardModel : IIdHasInt
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public int ProjectId { get; set; }

            //public ICollection<UserStory> UserStories { get; set; }
        }

        public class AddBoardModel
        {
            public string Title { get; set; }
            public int ProjectId { get; set; }
        }

        public class EditBoardModel : IIdHasInt
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}
