using System;
using Common.Interfaces;

namespace DAL.Entities
{
    public class News : IIdHas<int>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public User Author { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
