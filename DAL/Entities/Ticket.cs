using Common.Enum;
using Common.Interfaces;
using System;

namespace DAL.Entities
{
    public class Ticket : IIdHasInt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public TicketStatusType Status { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public int UserStoryid { get; set; }
    }
}
