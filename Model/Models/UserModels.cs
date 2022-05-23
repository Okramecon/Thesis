using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public static class UserModels
    {
        public class AddIn
        {
            /// <summary>
            /// Логин
            /// </summary>
            [Required]
            public string UserName { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            [Required]
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия
            /// </summary>
            [Required]
            public string LastName { get; set; }

            /// <summary>
            /// Пароль
            /// </summary>
            [Required]
            public string Password { get; set; }

            [Compare(nameof(Password), ErrorMessage = "Passord and confirmation do not match")]
            public string PasswordConfirmation { get; set; }
        }

        public class EditIn
        {
            /// <summary>
            /// Имя
            /// </summary>
            [Required]
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия
            /// </summary>
            [Required]
            public string LastName { get; set; }
        }

        public class ByIdOut 
        {
            /// <summary>
            /// Id
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Логин
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия
            /// </summary>
            public string LastName { get; set; }

            public string Roles { get; set; }
        }

        public class ListOut
        {
            /// <summary>
            /// Логин
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// Код
            /// </summary>
            public string Id { get; set; }
        }
    }
}