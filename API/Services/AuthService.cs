using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace API.Services
{
    public class AuthService
    {
        private UserManager<User> UserManager { get; set; }

        private AuthModel.JwtSettings Jwt { get; set; }

        public AuthService(UserManager<User> userManager, IOptions<AuthModel.JwtSettings> jwt)
        {
            UserManager = userManager;
            Jwt = jwt.Value;
        }

        public async Task<AuthModel.Response> AccessToken(AuthModel.Login model)
        {
            var (user, claims, roleNames) = await GetUserClaimsRoleNames(model.UserName, model.Password);
            return BuildResponse(user, claims, roleNames);
        }

        private AuthModel.Response BuildResponse(User user, IEnumerable<Claim> claims, IEnumerable<string> roleNames)
        {
            var (accessToken, expireDate) = GetAccessToken(claims);

            return new AuthModel.Response
            {
                AccessToken = accessToken,
                AccessTokenExpireDate = expireDate,
                Roles = string.Join(',', roleNames),
                UserName = user.UserName
            };
        }

        private async Task<(User, IEnumerable<Claim>, IEnumerable<string>)> GetUserClaimsRoleNames(string userName, string password)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (!(await UserManager.CheckPasswordAsync(user, password)))
            {
                throw new Exception($"2. Неверный пароль");
            }
            var (claims, roleNames) = await GetClaimsRoleNames(user);

            return (user, claims, roleNames);
        }

        private async Task<(IEnumerable<Claim>, IList<string>)> GetClaimsRoleNames(User user)
        {
            var roles = await UserManager.GetRolesAsync(user);
            var claims = new List<Claim>

            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return (claims, roles);
        }

        private (string, DateTime) GetAccessToken(IEnumerable<Claim> claims)
        {
            var utcNow = DateTime.UtcNow;
            var expire = utcNow.Add(TimeSpan.FromMinutes(Jwt.AccessTokenLifeTimeInMinutes));
            var jwt = new JwtSecurityToken(
                Jwt.Issuer,
                Jwt.Audience,
                claims,
                utcNow,
                expire,
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key)), SecurityAlgorithms.HmacSha256Signature));
            return (new JwtSecurityTokenHandler().WriteToken(jwt), expire);
        }
    }
}