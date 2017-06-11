using ATPTennisStat.ReportGenerators.Enums;

namespace ATPTennisStat.ReportGenerators.Contracts
{
    public interface IReportGenerator
    {
        void GenerateReport(PdfReportType reportType);
    }
}
