using Chat.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;

        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public async Task SendTo(SendMsgModel message, string to)
        {
            var user = await _userManager.FindByIdAsync(to);
            if (user is null)
                return;
        }
    }
}
