using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands;
using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.ReportGenerators.Enums;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class CreateRankingPdf_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfCreateRankingPdfClass_WhenThePassedValuesAreValid()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateRankingPdf(reporterMock.Object, loggerMock.Object);

            Assert.IsInstanceOf<CreateRankingPdf>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedReporterIsNull()
        {
            var loggerMock = new Mock<ILogger>();

            Assert.Throws<ArgumentNullException>(() => new CreateRankingPdf(null, loggerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedLoggerIsNull()
        {
            var reporterMock = new Mock<IReportGenerator>();

            Assert.Throws<ArgumentNullException>(() => new CreateRankingPdf(reporterMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ReturnSuccess_WhenReporterAndLoggerAreValid()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateRankingPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>());

            Assert.That(result.Contains("Successfully created ranking report"));
        }

        [Test]
        public void ExecuteShould_CallReporterGenerateReportOnce()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateRankingPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>());

            reporterMock.Verify(x => x.GenerateReport(PdfReportType.Ranking), Times.Once);
        }

        [Test]
        public void ExecuteShould_CallLoggerLogOnce()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateRankingPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>());

            loggerMock.Verify(x => x.Log("Successfully created ranking report"), Times.Once);
        }
    }
}