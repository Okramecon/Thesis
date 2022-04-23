using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace Chat
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }
    }
}
