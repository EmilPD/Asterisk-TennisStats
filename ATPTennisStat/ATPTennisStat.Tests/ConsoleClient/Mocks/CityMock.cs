using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    class CityMock : City
    {
        public CityMock() : base()
        {
        }

        new public int Id { get; set; }

        new public string Name { get; set; }

        new public Country Country { get; set;}
    }
}