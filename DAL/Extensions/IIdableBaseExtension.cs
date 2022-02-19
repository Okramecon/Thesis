using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extensions
{
    public static class IIdableBaseExtension
    {
        public static async Task<T> ById<T>(this IQueryable<T> query, int id) where T : class, IIdHas<int>
        {
            return await query.ById<T, int>(id);
        }

        public static async Task<T> ById<T, TKey>(this IQueryable<T> entities, TKey id)
            where T : class, IIdHas<TKey>
            where TKey : IEquatable<TKey>
        {
            return await entities.FirstOrDefaultAsync(x => x.Id.Equals(id))
                   ?? throw new Exception($"Сущность '{typeof(T).Name}' с кодом = '{id}' не найдена.");
        }
    }
}
