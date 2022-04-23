using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ChatRoomUser : IIdHas<int>
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [ForeignKey("ChatRoomId")]
        public int ChatRoomId { get; set; }
        public virtual User User { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
