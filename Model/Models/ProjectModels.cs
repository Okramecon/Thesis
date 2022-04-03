using Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Model.Models.BoardModels;

namespace Model.Models
{
    public static class ProjectModels
    {
        public class GetProjectModel : IIdHasInt
        {
            public int Id { get; set; }
            [MaxLength(20)]
            public string Title { get; set; }
            [MaxLength(100)]
            public string Summary { get; set; }
            public int DepartmentId { get; set; }

            //public ICollection<Board> Boards { get; set; }
        }

        public class GetDetailProjectModel : GetProjectModel
        {
            public ICollection<GetDetailBoardModel> Boards { get; set; }
        }

        public class AddProjectModel
        {
            public string Title { get; set; }
            [MaxLength(100)]
            public string Summary { get; set; }
            public int DepartmentId { get; set; }
        }

        public class EditProjectModel : IIdHasInt
        {
            [Required]
            public int Id { get; set; }
            public string Title { get; set; }
            [MaxLength(100)]
            public string Summary { get; set; }
        }
    }
}
