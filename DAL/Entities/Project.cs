using Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Project : IIdHas<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<Board> Boards { get; set; }
    }
}
