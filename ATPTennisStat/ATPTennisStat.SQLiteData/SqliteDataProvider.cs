using System;
using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLiteData
{
    public class SqliteDataProvider : ISqliteDataProvider
    {
        private IRepository<Log> logs;
        private IRepository<LogDetail> logDetails;

        private IUnitOfWork unitOfWork;

        public SqliteDataProvider(IUnitOfWork uow, 
                                  IRepository<Log> logs,
                                  IRepository<LogDetail> logDetails)
        {
            this.logs = logs;
            this.logDetails = logDetails;
            this.UnitOfWork = uow;
        }

        public IRepository<LogDetail> LogDetails
        {
            get
            {
                return this.logDetails;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Logs");
                }

                this.logDetails = value;
            }
        }

        public IRepository<Log> Logs
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
        
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Unit of work");
                }

                this.unitOfWork = value;
            }
        }
    }
}