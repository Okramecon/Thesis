using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static Model.Models.ChatRoomModels;

namespace BLL.Services
{
    public class ChatMessageService
    {
        private readonly AppDbContext appDbContext;
        private readonly ChatRoomService chatRoomService;
        private readonly UserManager<User> userManager;

        private DbSet<ChatMessage> ChatMessages { get; set; }

        public ChatMessageService(AppDbContext context, ChatRoomService chatRoomService, UserManager<User> userManager)
        {
            appDbContext = context;
            this.chatRoomService = chatRoomService;
            this.userManager = userManager;
            ChatMessages = context.ChatMessages;
        }

        public async Task<int> Add(ChatMessage message)
        {
            if (message.ChatRoomId == null)
            {
                var roomId = await chatRoomService.Add(new AddRoomModel());
                message.ChatRoomId = roomId;
                var from = await userManager.FindByNameAsync(message.FromId);
                var to = await userManager.FindByNameAsync(message.ToId);

                await appDbContext.ChatRoomUsers.AddAsync(new ChatRoomUser()
                {
                    UserId = from.Id,
                    ChatRoomId = roomId
                });
                await appDbContext.ChatRoomUsers.AddAsync(new ChatRoomUser()
                {
                    UserId = to.Id,
                    ChatRoomId = roomId
                });
            }

            message.SendDateTime = DateTime.Now;
            await ChatMessages.AddAsync(message);
            await appDbContext.SaveChangesAsync();
            return message.Id;
        }
    }
}
