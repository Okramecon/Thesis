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
using Model.Models;

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

        public async Task<List<UserModels.ByIdOut>> GetUsersByDepartment(int departmentId)
        {
            var entity = await Entities
                .Include(x => x.Users)
                .ById(departmentId);

            var result = entity.Users.Adapt<List<UserModels.ByIdOut>>();
            foreach(var item in result)
            {
                var user = await UserManager.FindByIdAsync(item.Id);
                var roles = await UserManager.GetRolesAsync(user);
                item.Roles = string.Join(',', roles);
            }
            return result;
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

        public async Task RemoveUserFromDepartment(int departmentId, string userIdToDelete, string userId)
        {

            var department = await Entities
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == departmentId);

            if(department.Users.FirstOrDefault(x => x.Id == userId) == null)
            {
                throw new InnerException($"Can not remove user from department with id={departmentId}", "9afd5c6c-186c-441f-980a-e463dc720cda");
            }

            if (department.IsNull())
            {
                throw new InnerException($"No such department with id={departmentId}", "d0b68913-a305-4139-ab0f-204e63759e28");
            }

            var userToDelete = department.Users.FirstOrDefault(x => x.Id == userIdToDelete);
            department.Users.Remove(userToDelete);
            await Context.SaveChangesAsync();
        }
    }
}
