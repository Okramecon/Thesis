using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services.Bases;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using Common.Interfaces;
using DAL.EF;
using DAL.Entities;
using DAL.Extensions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DepartmentService : EntityService<Department, int>
    {
        private UserManager<User> UserManager { get; }

        public DepartmentService(AppDbContext context, UserManager<User> userManager) : base(context, context.Departments)
        {
            UserManager = userManager;
        }

        public async Task AddUserToDepartment(string userId, int departmentId)
        {
            var entity = await Entities
                .Include(x => x.Users)
                .ById(departmentId);

            var user = await UserManager.FindByIdAsync(userId.ToString());
            entity.Users.Add(user);

            await Context.SaveChangesAsync();
        }

        public async Task AddUserToDepartmentByEmail(string userName, int departmentId)
        {
            var entity = await Entities
                .Include(x => x.Users)
                .ById(departmentId);

            var user = await UserManager.FindByEmailAsync(userName);
            entity.Users.Add(user);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ListByUser<T>(string userId, ICollection<RoleType> roles)
        {
            if(roles.Contains(RoleType.Admin))
            {
                return await Entities
                    .ProjectToType<T>()
                    .ToListAsync();
            }

            var user = await Context.Users
                    .Include(x => x.Departments)
                    .ById(userId);

            return user.Departments.Adapt<IEnumerable<T>>();
        }

        public async Task<T> ById<T> (int departmentId, string userId, ICollection<RoleType> roles) where T: class, IIdHas<int>
        {
            if(roles.Contains(RoleType.Admin))
            {
                return await base.ById<T>(departmentId);
            }

            var user = await Context.Users
                    .Include(x => x.Departments)
                    .ById(userId);
            var department = user.Departments.FirstOrDefault(x => x.Id == departmentId);

            if(department.IsNull())
            {
                throw new InnerException($"No such department with id={departmentId}", "2e1cd1a5-43c8-4665-9a0b-fb6c2ff1aaaf");
            }

            return department.Adapt<T>();
        }
    }
}
