using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class UserStory : IIdHas<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int BoardId { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
