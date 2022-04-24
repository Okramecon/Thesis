using DAL.EF;
using DAL.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Models.ChatRoomModels;

namespace BLL.Services
{
    public class ChatRoomService
    {
        private readonly AppDbContext appDbContext;

        public DbSet<ChatRoom> ChatRooms { get; set; }

        public ChatRoomService(AppDbContext context)
        {
            appDbContext = context;
            ChatRooms = context.ChatRooms;
        }

        public async Task<int> Add(AddRoomModel model)
        {
            var room = model.Adapt<ChatRoom>();
            room.CreatedDateTime = DateTime.Now;
            await ChatRooms.AddAsync(room);
            await appDbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<GetChatRoomMessagesModel> ById(int id)
        {
            return (await ChatRooms
                .Include(cr => cr.ChatMessages)
                .Include(cr => cr.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(cr => cr.Id == id))
                .Adapt<GetChatRoomMessagesModel>();
        }
        
        public async Task<GetChatRoomMessagesModel> CommonChatRoom(string userId1, string userId2)
        {
            return (await ChatRooms
                .Include(cr => cr.ChatMessages)
                .Include(cr => cr.Users)
                .ThenInclude(cr => cr.User)
                .FirstOrDefaultAsync(cr =>
                    cr.Users.Any(u => u.UserId == userId1) &&
                    cr.Users.Any(u => u.UserId == userId2)))
                .Adapt<GetChatRoomMessagesModel>();
        }
        
        public async Task<ChatRoom> CommonChatRoomByUsername(string username1, string username2)
        {
            return await ChatRooms
                .Include(cr => cr.Users)
                .ThenInclude(cr => cr.User)
                .FirstOrDefaultAsync(cr =>
                    cr.Users.Any(u => u.User.UserName == username1) &&
                    cr.Users.Any(u => u.User.UserName == username2));
        }

        public async Task<IEnumerable<GetChatRoomModel>> GetUserChatRooms(string username)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return await appDbContext.ChatRooms
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(x => x.Users.Any(u => u.UserId == user.Id))
                .ProjectToType<GetChatRoomModel>()
                .ToListAsync();
        }
    }
}
