using Common.Interfaces;
using System;

namespace Model.Models
{
    public static class UserStoryModels
    {
        public class GetUserStoryModel : IIdHasInt
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public int BoardId { get; set; }

            //public ICollection<Ticket> Tickets { get; set; }
        }

        public class AddUserStoryModel
        {
            public string Title { get; set; }
            public string Details { get; set; }
            public int BoardId { get; set; }
        }

        public class EditUserStoryModel : IIdHasInt
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
        }
    }
}
