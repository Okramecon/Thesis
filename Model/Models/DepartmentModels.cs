﻿using Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public static class DepartmentModels
    {
        public class GetDepartmentModel : IIdHasInt
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }

            //public ICollection<Project> Projects { get; set; }
        }

        public class AddDepartmentModel
        {
            public string Title { get; set; }
            public string Summary { get; set; }
        }

        public class EditDepartmentModel : IIdHasInt
        {
            [Required]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }
        }
    }
}
