using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class SendMsgModel
    {
        public string Message { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public int? ReplyTo { get; set; }
    }
}
