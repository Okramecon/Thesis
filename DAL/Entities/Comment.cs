using Common.Interfaces;
using System;

namespace DAL.Entities
{
    public class Comment : IIdHasInt
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }

        public User User { get; set; }
    }
}
