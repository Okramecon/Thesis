using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Model.Models
{
    public class CommentModels
    {
        public class GetCommentModel : IIdHas<int>
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public int TicketId { get; set; }
            public string Message { get; set; }
            public DateTime SendTime { get; set; }

            public UserModels.ByIdOut User { get; set; }

            public List<MediaModels.ListOut> Attachments { get; set; }
        }

        public class AddCommentModel
        {
            public string UserId { get; set; }
            public int TicketId { get; set; }
            public string Message { get; set; }

            public List<Guid> Attachments { get; set; }
        }
    }
}
