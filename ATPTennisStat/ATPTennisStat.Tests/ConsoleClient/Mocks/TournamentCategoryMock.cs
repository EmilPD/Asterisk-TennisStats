using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    class TournamentCategoryMock : TournamentCategory
    {
        public TournamentCategoryMock() : base()
        {

        }

        public int Id { get; set; }

        public string Category { get; set; }

        public byte PlayersCount { get; set; }
    }
}
