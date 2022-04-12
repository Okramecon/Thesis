using System;
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

        public async Task<NewsModels.ById> GetById(int id)
        {
            var entity = await News.ById(id);
            return entity.Adapt<NewsModels.ById>();
        }

        public async Task<int> Add(NewsModels.Add model, string id)
        {
            var entity = model.Adapt<News>();

            entity.CreatedDateTime = DateTime.UtcNow;
            entity.Author.Id = id;
            News.Add(entity);

            await AppDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
