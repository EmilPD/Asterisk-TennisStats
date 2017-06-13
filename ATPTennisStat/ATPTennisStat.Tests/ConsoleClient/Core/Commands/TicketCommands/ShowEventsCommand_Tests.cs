using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Tests.ConsoleClient.Mocks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.TicketCommands
{
    [TestFixture]
    class ShowEventsCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfShowEventsCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new ShowEventsCommand(providerMock.Object, writerMock.Object);

            Assert.IsInstanceOf<ShowEventsCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new ShowEventsCommand(null, writerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<IPostgresDataProvider>();

            Assert.Throws<ArgumentNullException>(() => new ShowEventsCommand(providerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();

            providerMock.Setup(p => p.TennisEvents.GetAll()).Returns(new TennisEvent[] { It.IsAny<TennisEvent>() });
            providerMock.Setup(p => p.Tickets.Find(It.IsAny<Expression<Func<Ticket, bool>>>())).Returns(new Ticket[] { new TicketMock() { Number = 1 } });

            var command = new ShowEventsCommand(providerMock.Object, writerMock.Object);
            try
            {
                command.Execute(new List<string>());
            }
            catch { }
            writerMock.Verify(x => x.Clear(), Times.Once);
        }
    }
}