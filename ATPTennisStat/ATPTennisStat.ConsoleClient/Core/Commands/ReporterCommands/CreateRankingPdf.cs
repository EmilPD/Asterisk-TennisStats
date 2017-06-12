using System;
using System.Collections.Generic;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ReportGenerators.Enums;
using ATPTennisStat.ReportGenerators.Contracts;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands
{
    public class CreateRankingPdf : ICommand
    {
        private const string Success = "Successfully created ranking report";
        private IReportGenerator reporter;
        private ILogger logger;

        public CreateRankingPdf(IReportGenerator reporter, ILogger logger)
        {
            if (reporter == null)
            {
                throw new ArgumentNullException("PdfReportGenerator");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.reporter = reporter;
            this.logger = logger;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Execute parameters");
            }

            this.reporter.GenerateReport(PdfReportType.Ranking);
            this.logger.Log(Success);
            return Success;
        }
    }
}