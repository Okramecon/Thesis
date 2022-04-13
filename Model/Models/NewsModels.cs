
using System;

namespace Model.Models
{
    public static class NewsModels
    {
        public class ById
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Body { get; set; }

            public UserModels.ByIdOut Author { get; set; }

            public DateTime CreatedDateTime { get; set; }

            public int DepartmentId { get; set; }
        }

        public class Add
        {
            public string Title { get; set; }

            public string Body { get; set; }

            public int DepartmentId { get; set; }
        }
    }
}
