using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Comment : IIdHas<int>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }

        public User User { get; set; }

        public List<Media> Attachments { get; set; }
    }
}
