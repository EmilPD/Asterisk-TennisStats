using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ATPTennisStat.Repositories.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetAllQuerable();
        
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}