using Common.Enums;
using Common.Exceptions;
using DAL.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private UserManager<User> Users { get; }

        public UserService(UserManager<User> users)
        {
            Users = users;
        }

        public async Task<string> RegisterAsync(UserModel.AddIn model)
        {
            if ((await Users.FindByNameAsync(model.UserName)) != null)
            {
                throw new InnerException("User Already exists", "fb8898d9-a414-4229-97af-14a7de5eee5d");
            }

            var result = await Users.CreateAsync(model.Adapt<User>(), model.Password);

            if (!result.Succeeded)
            {
                throw new InnerException("Invalid login or password", "8a9dc3a1-7c14-4339-af45-fb21f71f8a01");
            }

            var user = await Users.FindByNameAsync(model.UserName);

            var roleAddResult = await Users.AddToRolesAsync(user, new List<string> { RoleType.User.ToString() });

            if (!roleAddResult.Succeeded)
            {
                throw new InnerException("Error adding roles to user", "fc21b6c1-9cd5-42e7-84c9-f64f93dbb731");
            }

            return user.Id;
        }

        public async Task Edit<T>(string id, T model)
        {
            var user = await Users.FindByIdAsync(id);
            user = model.Adapt(user);
            await Users.UpdateAsync(user);
        }

        public async Task<T> GetById<T>(string id)
        {
            var user = await Users.FindByIdAsync(id);
            return user.Adapt<T>();
        }

        public async Task<IEnumerable<T>> List<T>()
        {
            var lst = await Users.Users
                .AsNoTracking()
                .ProjectToType<T>()
                .ToListAsync();

            return lst;
        }
    }
}