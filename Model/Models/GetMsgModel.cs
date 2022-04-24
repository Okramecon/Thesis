using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class GetMsgModel
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
