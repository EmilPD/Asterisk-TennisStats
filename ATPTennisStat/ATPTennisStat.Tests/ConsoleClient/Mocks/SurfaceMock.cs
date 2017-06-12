using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    public class SurfaceMock : Surface
    {
        public SurfaceMock() : base()
        {

        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Speed { get; set; }
    }
}
