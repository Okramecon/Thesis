using System;
using Microsoft.AspNetCore.Http;

namespace Model.Models
{
    public static class MediaModels
    {
        public class ListOut
        {
            public Guid Id { get; set; }

            public string ContentType { get; set; }

            public string Extension { get; set; }

            public string FileName { get; set; }
        }

        public class AddIn
        {
            public Guid Id { get; set; }
        }
    }
}
