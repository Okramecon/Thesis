using Common.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Department : IIdHasInt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
