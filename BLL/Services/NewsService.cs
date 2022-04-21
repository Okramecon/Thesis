using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace BLL.Services
{
    public class NewsService
    {
        public AppDbContext AppDbContext { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<User> Users { get; set; }

        public NewsService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            News = appDbContext.News;
            Users = appDbContext.Users;
        }

        public async Task<List<NewsModels.ById>> GetByDepartmentId(int departmentId)
        {
            var news = await News
                .Where(x => x.DepartmentId == departmentId)
                .OrderByDescending(x => x.CreatedDateTime)
                .ProjectToType<NewsModels.ById>()
                .ToListAsync();

            return news;
        }

        public async Task<int> Add(NewsModels.Add model, string id)
        {
            var entity = model.Adapt<News>();

            entity.CreatedDateTime = DateTime.UtcNow;

            entity.Author = await Users.ById(id);
            News.Add(entity);
           
            await AppDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
