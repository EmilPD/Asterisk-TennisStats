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
    class CreateMatchesPdf_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfCreateMatchesPdfClass_WhenThePassedValuesAreValid()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateMatchesPdf(reporterMock.Object, loggerMock.Object);

            Assert.IsInstanceOf<CreateMatchesPdf>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedReporterIsNull()
        {
            var loggerMock = new Mock<ILogger>();

            Assert.Throws<ArgumentNullException>(() => new CreateMatchesPdf(null, loggerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedLoggerIsNull()
        {
            var reporterMock = new Mock<IReportGenerator>();

            Assert.Throws<ArgumentNullException>(() => new CreateMatchesPdf(reporterMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ReturnSuccess_WhenReporterAndLoggerAreValid()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateMatchesPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>() { "some" });

            Assert.That(result.Contains("Successfully created matches report"));
        }

        [Test]
        public void ExecuteShould_CallReporterGenerateReportOnce()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateMatchesPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>() { "some" });

            reporterMock.Verify(x => x.GenerateReport(PdfReportType.Matches), Times.Once);
        }

        [Test]
        public void ExecuteShould_CallLoggerLogOnce()
        {
            var reporterMock = new Mock<IReportGenerator>();
            var loggerMock = new Mock<ILogger>();
            var command = new CreateMatchesPdf(reporterMock.Object, loggerMock.Object);

            string result = command.Execute(new List<string>() { "some" });

            loggerMock.Verify(x => x.Log("Successfully created matches report"), Times.Once);
        }
    }
}