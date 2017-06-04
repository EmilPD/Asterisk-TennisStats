using System;

namespace ATPTennisStat.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int Finished();
    }
}
