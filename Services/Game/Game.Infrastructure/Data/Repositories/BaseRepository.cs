﻿using Game.Application.Interfaces.Persistence;
using Game.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data.Repositories
{
    public class BaseRepository<T,TId> : IAsyncRepository<T,TId> where T : Entity<TId>,IAggregateRoot
    {
        protected readonly TriviaDbContext DbContext;

        public BaseRepository( TriviaDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(TId id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
        {
            return await DbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
        
    }
}
