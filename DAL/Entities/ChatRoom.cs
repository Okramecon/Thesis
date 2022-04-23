using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class ChatRoom : IIdHas<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IEnumerable<ChatMessage> ChatMessages { get; set; }
        public virtual IEnumerable<ChatRoomUser> Users { get; set; }
    }
}
