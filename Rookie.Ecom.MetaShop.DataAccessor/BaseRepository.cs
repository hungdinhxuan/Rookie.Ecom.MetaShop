using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.DataAccessor.Data;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.DataAccessor
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;


        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Remove<T>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbContext.Set<T>()
                .FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            // lets assume the property exists and is a nullable bool; get the value from the property.

            var property = entity.GetType().GetProperty("IsDeleted");

            property.SetValue(entity, true);

            await _dbContext.SaveChangesAsync();

        }
    }
}