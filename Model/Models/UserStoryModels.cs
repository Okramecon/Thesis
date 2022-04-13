using Common.Interfaces;
using System;
using System.Collections.Generic;
using static Model.Models.TicketModels;

namespace Model.Models
{
    public static class UserStoryModels
    {
        public class GetUserStoryModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public int BoardId { get; set; }
        }

        public class GetDetailUserStoryModel : GetUserStoryModel
        {
            public ICollection<GetTicketModel> Tickets { get; set; }
        }

        public class AddUserStoryModel
        {
            public string Title { get; set; }
            public string Details { get; set; }
            public int BoardId { get; set; }
        }

        public class EditUserStoryModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
        }
    }
}
