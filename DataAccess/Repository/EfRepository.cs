using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteUnity.DataAccess.Abstraction;

namespace WhiteUnity.DataAccess.EntityFramework
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector)
        {
            return _context.Set<TEntity>().Where(selector).AsQueryable();
        }

        public async Task Add(TEntity newEntity)
        {
            await _context.Set<TEntity>().AddAsync(newEntity);
        }

        public async Task Add(IEnumerable<TEntity> newEntities)
        {
            await _context.Set<TEntity>().AddRangeAsync(newEntities);
        }

        public async Task Remove(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Remove(entity));
        }

        public async Task Remove(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities));
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var updatedEntity = await Task.Run(() => _context.Set<TEntity>().Update(entity));
            return updatedEntity.Entity;
        }

        public async Task Update(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _context.Set<TEntity>().UpdateRange(entities));
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}