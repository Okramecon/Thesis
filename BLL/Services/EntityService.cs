using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
using DAL.EF;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Bases
{
    public abstract class EntityService<TEntity, TKey>
         where TEntity : class, IIdHas<TKey>
         where TKey : IEquatable<TKey>
    {
        protected DbSet<TEntity> Entities { get; set; }
        public AppDbContext Context { get; set; }

        protected EntityService(AppDbContext context, DbSet<TEntity> entities)
        {
            Entities = entities;
            Context = context;
        }

        public virtual async Task<TKey> Add<T>(T model)
        {
            var entity = model.Adapt<TEntity>();
            await BeforeAdd(entity);
            await Check(entity);
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        protected virtual Task BeforeAdd(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task Check(TEntity entity)
        {
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<T>> List<T>()
        {
            return await Entities
                .ProjectToType<T>()
                .ToListAsync();
        }

        //public virtual async Task<(IEnumerable<T> Data, int Total)> Page<T>(int number, int size)
        //{
        //    return (await Entities
        //         .Page(number, size)
        //         .ProjectToType<T>()
        //         .ToListAsync(), await Total(Entities, size));
        //}

        protected async Task<int> Total(IQueryable<TEntity> query, int size)
        {
            var count = await query
                .CountAsync();
            return count == 0
                ? 1
                : count / size + (count % size > 0 ? 1 : 0);
        }

        public virtual async Task<T> ById<T>(TKey id) where T : class, IIdHas<TKey>
        {
            return await Entities
                .ProjectToType<T>()
                .ById(id);
        }

        public virtual async Task Edit<T>(T model) where T : IIdHas<TKey>
        {
            var entity = await Entities.ById(model.Id);
            model.Adapt(entity);
            await Check(entity);

            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(TKey id)
        {
            Entities.Remove(await Entities.ById(id));
            await Context.SaveChangesAsync();
        }
    }
}
