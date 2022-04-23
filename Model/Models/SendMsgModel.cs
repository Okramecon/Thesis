using System;

namespace Model.Models
{
    public class SendMsgModel
    {
        public int ChatRoomId { get; set; }
        public string Message { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public int? ReplyTo { get; set; }
    }
}
