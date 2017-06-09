using System;
using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLiteData
{
    public class SqliteDataProvider
    {
        private IRepository<Log> logs;
        private Func<IUnitOfWork> unitOfWork;

        public SqliteDataProvider(Func<IUnitOfWork> uow, IRepository<Log> logs)
        {
            this.logs = logs;
            this.unitOfWork = uow;
        }

        public IRepository<Log> Tickets
        {
            get
            {
                return this.logs;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Logs");
                }

                this.logs = value;
            }
        }
        
        public Func<IUnitOfWork> UnitOfWork
        {
            get
            {
                return unitOfWork;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Unit of work");
                }

                unitOfWork = value;
            }
        }
    }
}