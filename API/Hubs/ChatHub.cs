using BLL.Services;
using DAL.Entities;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Model.Models.ChatRoomModels;

namespace API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly ChatMessageService _chatMessageService;
        private readonly ChatRoomService _chatRoomService;
        private readonly static Dictionary<string, string> _connectionsMap = new();
        //private readonly static List<User> _connections = new();
        private readonly static List<string> _connections = new();

        public ChatHub(
            UserManager<User> userManager,
            ChatMessageService chatMessageService,
            ChatRoomService chatRoomService)
        {
            _userManager = userManager;
            _chatMessageService = chatMessageService;
            _chatRoomService = chatRoomService;
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public async Task SendTo(SendMsgModel msgModel)
        {
            if (string.IsNullOrEmpty(msgModel.Message.Trim()))
                return;

            var message = msgModel.Adapt<ChatMessage>();

            await _chatMessageService.Add(message);

            await Clients.Caller.SendAsync("newMessage", message);
            await Clients.User(message.ToId).SendAsync("newMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(IdentityName);
                
                if (!_connections.Any(u => u == IdentityName))
                {
                    _connections.Add(IdentityName);
                }

                await Clients.Caller.SendAsync("getProfileInfo", user.Adapt<UserModels.ByIdOut>());
            }
            catch (System.Exception ex)
            {
                await Clients .Caller.SendAsync("onError", $"OnConnected: {ex.Message}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _connections.Remove(IdentityName);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", $"OnDisconnected: {ex.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public string IdentityName { get => Context.User.Identity.Name; }
    }
}
