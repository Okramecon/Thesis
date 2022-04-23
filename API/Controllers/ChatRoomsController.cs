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
        public async Task<GetChatRoomModel> ById(int chatId)
        {
            return await chatRoomService.ById(chatId);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IEnumerable<GetChatRoomModel>> GetUserChats(string username)
        {
            var test =  await chatRoomService.GetUserChats(username);
            return test;
        }
    }
}
