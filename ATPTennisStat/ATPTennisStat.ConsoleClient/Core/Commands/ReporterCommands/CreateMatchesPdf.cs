using System;
using System.Collections.Generic;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ReportGenerators.Enums;
using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands
{
    public class CreateMatchesPdf : ICommand
    {
        private const string Success = "Successfully created matches report";
        private IReportGenerator reporter;
        private ILogger logger;

        public CreateMatchesPdf(IReportGenerator reporter, ILogger logger)
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

        public string Execute()
        {
            this.reporter.GenerateReport(PdfReportType.Matches);
            this.logger.Log(Success);
            return Success;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                throw new ArgumentException(Messages.ParametersWarning);
            }
        }
    }
}