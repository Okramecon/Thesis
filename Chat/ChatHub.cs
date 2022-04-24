using BLL.Services;
using Chat.Models;
using Common.Enums;
using DAL.Entities;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly ChatMessageService _chatMessageService;

        public ChatHub(UserManager<User> userManager, ChatMessageService chatMessageService)
        {
            _userManager = userManager;
            _chatMessageService = chatMessageService;
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public async Task SendTo(SendMsgModel message)
        {
            var user = await _userManager.FindByNameAsync(message.ToId);
            if (user is null)
                return;

            var u = Context.UserIdentifier;

            var messageEntity = message.Adapt<ChatMessage>();
            await _chatMessageService.Add(messageEntity);

            if (Context.UserIdentifier != message.ToId) // если получатель и текущий пользователь не совпадают
                await Clients.User(Context.UserIdentifier).SendAsync("Receive", message);
            await Clients.User(message.ToId).SendAsync("Receive", message);

            //await Clients.User(message.ToId).SendAsync("Recieve", messageEntity.Adapt<SendMsgModel>(), );
            //await Clients.All.SendAsync("ReceiveMessage", messageEntity.Adapt<SendMsgModel>());
        }
    }
}
