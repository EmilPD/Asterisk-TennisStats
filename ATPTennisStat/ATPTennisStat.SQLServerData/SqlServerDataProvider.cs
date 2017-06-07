using System;
using ATPTennisStat.Models;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLServerData
{
    public class SqlServerDataProvider
    {
        private IRepository<City> cities;
        private IRepository<Country> countries;
        private IRepository<Match> matches;
        private IRepository<Player> players;
        private IRepository<PointDistribution> pointDistributions;
        private IRepository<Round> rounds;
        private IRepository<Surface> surfaces;
        private IRepository<Tournament> tournaments;
        private IRepository<TournamentCategory> tournamentCategories;
        private IUnitOfWork unitOfWork;

        public SqlServerDataProvider(
            IUnitOfWork unitOfWork,
            IRepository<City> cities,
            IRepository<Country> countries,
            IRepository<Match> matches,
            IRepository<Player> players,
            IRepository<PointDistribution> pointDistributions,
            IRepository<Round> rounds,
            IRepository<Surface> surfaces,
            IRepository<Tournament> tournaments,
            IRepository<TournamentCategory> tournamentCategories)
        {
            this.UnitOfWork = unitOfWork;
            this.Cities = cities;
            this.Countries = countries;
            this.Matches = matches;
            this.Players = players;
            this.PointDistributions = pointDistributions;
            this.Rounds = rounds;
            this.Surfaces = surfaces;
            this.Tournaments = tournaments;
            this.TournamentCategories = tournamentCategories;
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
                    throw new ArgumentNullException("UnitOfWork");
                }

                this.unitOfWork = value;
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return this.cities;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cities");
                }

                this.cities = value;
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                return this.countries;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Countries");
                }

                this.countries = value;
            }
        }

        public IRepository<Match> Matches
        {
            get
            {
                return this.matches;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Matches");
                }

                this.matches = value;
            }
        }

        public IRepository<Player> Players
        {
            get
            {
                return this.players;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Players");
                }

                this.players = value;
            }
        }

        public IRepository<PointDistribution> PointDistributions
        {
            get
            {
                return this.pointDistributions;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("PointDistributions");
                }

                this.pointDistributions = value;
            }
        }

        public IRepository<Round> Rounds
        {
            get
            {
                return this.rounds;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Rounds");
                }

                this.rounds = value;
            }
        }

        public IRepository<Surface> Surfaces
        {
            get
            {
                return this.surfaces;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Surfaces");
                }

                this.surfaces = value;
            }
        }

        public IRepository<Tournament> Tournaments
        {
            get
            {
                return this.tournaments;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Tournaments");
                }

                this.tournaments = value;
            }
        }

        public IRepository<TournamentCategory> TournamentCategories
        {
            get
            {
                return this.tournamentCategories;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("TournamentCategories");
                }

                this.tournamentCategories = value;
            }
        }
    }
}