
using System;

namespace Model.Models
{
    public static class NewsModels
    {
        public class ByIdOut
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Body { get; set; }

            public UserModel.ByIdOut Author { get; set; }

            public DateTime CreatedDateTime { get; set; }
        }

        public class AddIn
        {
            public string Title { get; set; }

            public string Body { get; set; }
        }
    }
}
