using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.Repositories.Contracts;
using System;

namespace ATPTennisStat.Common
{
    public class PostgresDataProvider
    {
        private IRepository<Ticket> tickets;
        private IRepository<TennisEvent> tennisEvents;
        private Func<IUnitOfWork> unitOfWork;

        public PostgresDataProvider(Func<IUnitOfWork> uow, IRepository<Ticket> tickets, IRepository<TennisEvent> tennisEvents)
        {
            this.tickets = tickets;
            this.tennisEvents = tennisEvents;
            this.unitOfWork = uow;
        }

        public IRepository<Ticket> Tickets
        {
            get { return this.tickets; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Tickets");
                }

                this.tickets = value;
            }
        }

        public IRepository<TennisEvent> TennisEvents
        {
            get { return this.tennisEvents; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("TennisEvents");
                }

                this.tennisEvents = value;
            }
        }

        public Func<IUnitOfWork> UnitOfWork
        {
            get { return unitOfWork; }
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
