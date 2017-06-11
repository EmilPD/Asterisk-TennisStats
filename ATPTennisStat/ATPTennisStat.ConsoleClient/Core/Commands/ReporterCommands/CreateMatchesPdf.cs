using System;
using System.Collections.Generic;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.ReportGenerators;
using ATPTennisStat.ReportGenerators.Enums;
using ATPTennisStat.ReportGenerators.Contracts;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands
{
    class CreateMatchesPdf : ICommand
    {
        private const string Success = "Successfully created matches report";
        private IReportGenerator reporter;
        private IWriter writer;
        private ILogger logger;

        public CreateMatchesPdf(IReportGenerator reporter, ILogger logger, IWriter writer)
        {
            if (reporter == null)
            {
                throw new ArgumentNullException("PdfReportGenerator");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.reporter = reporter;
            this.writer = writer;
            this.logger = logger;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Execute parameters");
            }

            this.reporter.GenerateReport(PdfReportType.Matches);
            this.logger.Log(Success);
            return Success;
        }
    }
}