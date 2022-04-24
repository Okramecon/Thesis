using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Models.ChatRoomModels;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Chatrooms")]
    public class ChatRoomsController
    {
        private readonly ChatMessageService chatMessageService;
        private readonly ChatRoomService chatRoomService;

        public ChatRoomsController(ChatMessageService chatMessageService, ChatRoomService chatRoomService)
        {
            this.chatMessageService = chatMessageService;
            this.chatRoomService = chatRoomService;
        }

        [HttpGet]
        [Route("")]
        public async Task<GetChatRoomMessagesModel> ById(int chatId)
        {
            return await chatRoomService.ById(chatId);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IEnumerable<GetChatRoomModel>> GetUserChatRooms(string username)
        {
            return await chatRoomService.GetUserChatRooms(username);
        }

        [HttpGet]
        [Route("common")]
        public async Task<GetChatRoomMessagesModel> GetCommonChatRoom(string userId1, string userId2)
        {
            return await chatRoomService.CommonChatRoom(userId1, userId2);
        }
    }
}
