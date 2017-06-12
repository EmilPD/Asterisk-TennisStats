using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    class CountryMock : Country
    {
        public CountryMock() : base()
        {
        }

        new public int Id { get; set; }

        new public string Name { get; set; }
    }
}