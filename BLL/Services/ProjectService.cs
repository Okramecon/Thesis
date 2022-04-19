using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Models.ProjectModels;
using static Model.Models.TicketModels;

namespace BLL.Services
{
    public class ProjectService : EntityService<Project, int>
    {
        private MediaService MediaService { get; }

        public ProjectService(AppDbContext context, MediaService mediaService) : base(context, context.Projects)
        {
            MediaService = mediaService;
        }

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
            var tickets = project.Boards
                .SelectMany(x => x.Tickets);
            foreach(var ticket in tickets)
            {
                var mediaIds = ticket.Attachments.Select(x => x.Id).ToList();
                ticket.Attachments = await MediaService.GetList(mediaIds);
            }

            return tickets;
        }

        public override async Task<int> Add<T>(T model)
        {
            var entity = model.Adapt<Project>();
            entity.Boards = new List<Board>
            {
                new Board
                {
                    Title = string.Empty,
                    CreatedDateTime = DateTime.UtcNow
                }
            };
            Entities.Add(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
