using Common.Interfaces;
using System;

namespace DAL.Entities
{
    public class ChatMessage : IIdHas<int>
    {
        public int Id { get; set; }
        public int? ChatRoomId { get; set; }
        public string ToId { get; set; }
        public string FromId { get; set; }
        public string Message { get; set; }
        public DateTime SendDateTime { get; set; }
        public int? ReplyTo { get; set; }
    }
}
