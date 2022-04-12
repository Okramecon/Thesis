using Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Project : IIdHas<int>
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Summary { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<Board> Boards { get; set; }
    }
}
