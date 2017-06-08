using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        protected readonly DbSet<TEntity> dbSet;

        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required.", "context");
            }

            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet.ToList();
        }

        public IEnumerable<TEntity> GetAll(out DbSet dbSet)
        {
            dbSet = this.dbSet;
            return this.dbSet.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.Where(predicate).ToList();
        }
        
        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);

            var dbSetLocalToList = this.dbSet.Local.ToList();

            var entry = this.context.Entry<TEntity>(entity);
            var state = entry.State;
            //state = System.Data.Entity.EntityState.Modified;

            var dbSetToList = this.dbSet.AsEnumerable().ToList();
            var dbSet = this.dbSet;


        }
        
        public void Remove(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
