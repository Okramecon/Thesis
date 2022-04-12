﻿using Common.Interfaces;
using System;

namespace Model.Models
{
    public class CommentModels
    {
        public class GetCommentModel : IIdHasInt
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public int TicketId { get; set; }
            public string Message { get; set; }
            public DateTime SendTime { get; set; }

            public UserModel.ByIdOut User { get; set; }
        }

        public class AddCommentModel
        {
            public string UserId { get; set; }
            public int TicketId { get; set; }
            public string Message { get; set; }
        }



    }
}
