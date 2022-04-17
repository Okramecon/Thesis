using System;
using Microsoft.AspNetCore.Http;

namespace Model.Models
{
    public static class MediaModels
    {
        public class ListOut
        {
            public Guid Id { get; set; }

            public IFormFile File { get; set; }

            public string Extension { get; set; }
        }
    }
}
