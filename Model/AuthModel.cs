using System;

namespace Model
{
    public static class AuthModel
    {
        public class JwtSettings
        {
            public string Key { get; set; }

            public string Issuer { get; set; }

            public string Audience { get; set; }

            public int AccessTokenLifeTimeInMinutes { get; set; }

            public int RefreshTokenLifeTimeInMinutes { get; set; }
        }

        public class Login
        {
            /// <summary>
            /// Имя пользователя
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Пароль
            /// </summary>
            public string Password { get; set; }
        }

        public class Refresh
        {
            /// <summary>
            /// Рефреш токен
            /// </summary>
            public string RefreshToken { get; set; }
        }

        public class Response : Refresh
        {
            /// <summary>
            /// Токен доступа
            /// </summary>
            public string AccessToken { get; set; }

            /// <summary>
            /// Срок истечения токена доступа
            /// </summary>
            public DateTime AccessTokenExpireDate { get; set; }

            /// <summary>
            /// Срок истечения рефреш токена
            /// </summary>
            public DateTime RefreshTokenExpireDate { get; set; }

            /// <summary>
            /// Имя пользователя
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Роли пользователя
            /// </summary>
            public string Roles { get; set; }
        }
    }
}