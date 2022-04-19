using Common.Enum;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Ticket : IIdHas<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public TicketStatusType Status { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public List<Media> Attachments { get; set; }
    }
}
