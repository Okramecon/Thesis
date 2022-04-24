using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ChatRoomUserModels
    {
        public class OnlyUserModel
        {
            public string UserId { get; set; }
            public UserModels.ByIdOut User { get; set; }
        }
    }
}
