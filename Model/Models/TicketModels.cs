using Common.Enum;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public static class TicketModels
    {
        public class GetTicketModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public TicketStatusType Status { get; set; }
            public DateTime CreatedDatetime { get; set; }
            public int BoardId { get; set; }

            public List<MediaModels.ListOut> Attachments { get; set; }
        }

        public class AddTicketModel
        {
            public string Title { get; set; }
            public string Details { get; set; }
            public TicketStatusType Status { get; set; }
            public int BoardId { get; set; }

            public List<Guid> Attachments { get; set; }
        }

        public class EditTicketModel : IIdHas<int>
        {
            [Required]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public TicketStatusType Status { get; set; }

            public List<MediaModels.AddIn> Attachments { get; set; }
        }
    }
}
