using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Enum;
using Common.Exceptions;
using Common.Extensions;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TokenService
    {
        private AppDbContext AppDbContext { get; }

        private UserManager<User> UserManager { get; }

        public TokenService(AppDbContext appDbContext, UserManager<User> userManager)
        {
            AppDbContext = appDbContext;
            UserManager = userManager;
        }

        private const int EmailConfirmationTokenLength = 25;

        public async Task<string> CreateEmailToken(string userId)
        {
            var randomString = StringExtensions.GetUniqueKey(EmailConfirmationTokenLength);
            var token = new IdentityUserToken<string>
            {
                UserId = userId,
                Value = randomString,
                Name = TokenType.EmailConfirmation.ToString(),
                LoginProvider = randomString,
            };

            AppDbContext.UserTokens.Add(token);
            await AppDbContext.SaveChangesAsync();
            return token.Value;
        }

        public async Task HandleEmailToken(string tokenValue)
        {
            var token = AppDbContext.UserTokens.FirstOrDefault(t => t.Value == tokenValue && t.Name == TokenType.EmailConfirmation.ToString());

            //to do, add expiration date
            if(token.IsNull())
            {
                throw new InnerException("Token is invalid", "ee360308-0a28-4797-8a3d-7cad9ea97e93");
            }

            var user = await UserManager.FindByIdAsync(token.UserId);
            user.EmailConfirmed = true;
            AppDbContext.Remove(token);

            await AppDbContext.SaveChangesAsync();
        }

        public async Task<string> GetEmailTokenByUserId(string userId)
        {
            return (await AppDbContext.UserTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.Name == TokenType.EmailConfirmation.ToString()))?.Value;
        }
    }
}
