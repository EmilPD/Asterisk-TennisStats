using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    public class TournamentMock : Tournament
    {
        public TournamentMock() : base()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
