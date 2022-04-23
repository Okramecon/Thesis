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

        public async Task<GetChatRoomModel> ById(int id)
        {
            return (await ChatRooms
                .Include(cr => cr.ChatMessages)
                .Include(cr => cr.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(cr => cr.Id == id))
                .Adapt<GetChatRoomModel>();
        }

        public async Task<IEnumerable<GetChatRoomModel>> GetUserChats(string username)
        {
            var user = (await appDbContext.Users.ToListAsync()).FirstOrDefault(u => u.UserName == username);

            return await appDbContext.ChatRooms
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(x => x.Users.Any(u => u.UserId == user.Id))
                .ProjectToType<GetChatRoomModel>()
                .ToListAsync();
        }
    }
}
