using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using DAL.EF;
using DAL.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private UserManager<User> Users { get; }

        private TokenService TokenService { get; }

        private EmailService EmailService { get; }

        private AppDbContext AppDbContext { get; }

        public UserService(UserManager<User> users, TokenService tokenService, EmailService emailService, AppDbContext appDbContext)
        {
            Users = users;
            TokenService = tokenService;
            EmailService = emailService;
            AppDbContext = appDbContext;
        }

        public async Task<string> RegisterAsync(UserModels.AddIn model)
        {
            if(!model.UserName.IsValidEmail())
            {
                throw new InnerException("Not valid email", "b85d04c5-8b74-4f96-94af-77f141e2a9aa");
            }

            var existingUser = await Users.FindByNameAsync(model.UserName);

            if (existingUser != null)
            {
                if(!existingUser.EmailConfirmed)
                {
                    throw new InnerException("Confirmation code has been sent to your email", "fb8898d9-a414-4229-97af-14a7de5eee5d");
                }

                throw new InnerException("User with such email already exists", "fb8898d9-a414-4229-97af-14a7de5eee5d");
            }

            var user = model.Adapt<User>();
            user.Email = model.UserName;

            var result = await Users.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InnerException("Invalid login or password", "8a9dc3a1-7c14-4339-af45-fb21f71f8a01");
            }

            var roleAddResult = await Users.AddToRolesAsync(user, new List<string> { RoleType.User.ToString() });

            if (!roleAddResult.Succeeded)
            {
                throw new InnerException("Error adding roles to user", "fc21b6c1-9cd5-42e7-84c9-f64f93dbb731");
            }

            //create token

            var tokenValue = await TokenService.CreateEmailToken(user.Id);

            if(tokenValue.IsNull())
            {
                throw new InnerException("Error creating token", "ae758956-dc57-4fd3-8bd0-091d3a051c56");
            }


            //send confirmation code
            EmailService.SendEmailConfirmation(tokenValue, user.Email);

            return user.Id;
        }

        public async Task Edit<T>(string id, T model)
        {
            var user = await Users.FindByIdAsync(id);
            if (user.IsNull())
            {
                throw new InnerException($"No such user with id {id}", "844601d4-c37b-4602-abec-5eeb5e9c67db");
            }

            if (!user.EmailConfirmed)
            {
                throw new InnerException("Confirmation code has been sent to your email", "7658d877-825e-4ff7-a447-1d197fd23181");
            }

            user = model.Adapt(user);
            await Users.UpdateAsync(user);
        }

        public async Task<T> GetById<T>(string id)
        {
            var user = await Users.FindByIdAsync(id);
            if(user.IsNull())
            {
                throw new InnerException($"No such user with id {id}", "844601d4-c37b-4602-abec-5eeb5e9c67db");
            }
            return user.Adapt<T>();
        }
    }
}