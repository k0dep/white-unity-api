using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WhiteUnity.DataAccess.Abstraction
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector);

        Task Add(TEntity newEntity);
        Task Add(IEnumerable<TEntity> newEntities);
        
        Task Remove(TEntity entity);
        Task Remove(IEnumerable<TEntity> entities);
        
        Task<TEntity> Update(TEntity entity);
        Task Update(IEnumerable<TEntity> entities);

        IQueryable<TEntity> All();
    }
}