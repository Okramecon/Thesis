using System;

namespace Model.Models
{
    public static class UserModels
    {
        public class AddIn
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
            /// Пароль
            /// </summary>
            public string Password { get; set; }
        }

        public class EditIn
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