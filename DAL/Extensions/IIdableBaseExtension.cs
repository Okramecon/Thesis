using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extensions
{
    public static class IIdableBaseExtension
    {
        public static async Task<T> ById<T>(this IQueryable<T> query, int id) where T : class, IIdHasInt<int>
        {
            return await query.ById<T, int>(id);
        }

        public static async Task<T> ById<T, TKey>(this IQueryable<T> entities, TKey id)
            where T : class, IIdHasInt<TKey>
            where TKey : IEquatable<TKey>
        {
            return await entities.FirstOrDefaultAsync(x => x.Id.Equals(id))
                   ?? throw new InnerException($"Сущность '{typeof(T).Name}' с кодом = '{id}' не найдена.", "e5804923-bf86-43e1-8f16-68eb81d8cfef");
        }
    }
}
