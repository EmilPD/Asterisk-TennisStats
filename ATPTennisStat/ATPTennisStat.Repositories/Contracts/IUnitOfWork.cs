using System;
using System.Data.Entity.Infrastructure;

namespace ATPTennisStat.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Finished();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}