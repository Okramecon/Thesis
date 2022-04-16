using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Board : IIdHas<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
