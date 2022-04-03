﻿using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Models.ProjectModels;
using static Model.Models.TicketModels;

namespace BLL.Services
{
    public class ProjectService : EntityService<Project, int>
    {
        public ProjectService(AppDbContext context) : base(context, context.Projects) { }

        /// <summary>
        /// Get department-owned projects
        /// </summary>
        /// <param name="id">Department id</param>
        /// <returns>List of department-owned projects</returns>
        public async Task<IEnumerable<GetProjectModel>> GetProjectsByDepartmentId(int id)
        {
            return await Entities
                .Where(x => x.DepartmentId == id)
                .ProjectToType<GetProjectModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<GetTicketModel>> GetTicketsByProjectIdAsync(int id)
        {
            var project = await ById<GetDetailProjectModel>(id);
            return project.Boards
                .SelectMany(x => x.UserStories)
                .SelectMany(x => x.Tickets);
        }
    }
}
