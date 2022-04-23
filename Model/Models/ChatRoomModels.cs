using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ChatRoomModels
    {
        public class AddRoomModel
        {
            public string Name { get; set; } //identifier
            public DateTime CreatedDateTime { get; set; }
            public string GroupName { get; set; } // for chat hub
            public string AdminId { get; set; }
        }

        public class GetChatRoomModel
            {
            public int Id { get; set; }
            public string Name { get; set; } // Use for group chats (todo mb feature)
            public IEnumerable<ChatRoomUserModels.OnlyUserModel> Users { get; set; }
            public IEnumerable<GetMsgModel> ChatMessages { get; set; }
        }
    }
}
