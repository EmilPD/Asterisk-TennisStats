using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.SQLServerData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.TicketCommands
{
    [TestFixture]
    class BuyTicketCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfBuyTicketCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new BuyTicketsCommand(providerMock.Object, writerMock.Object);

            Assert.IsInstanceOf<BuyTicketsCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new BuyTicketsCommand(null, writerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<IPostgresDataProvider>();

            Assert.Throws<ArgumentNullException>(() => new BuyTicketsCommand(providerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();
            providerMock.Setup(p => p.Tickets.Find(It.IsAny<Expression<Func<Ticket, bool>>>())).Returns(new Ticket[] { It.IsAny<Ticket>() });

            var command = new BuyTicketsCommand(providerMock.Object, writerMock.Object);

            command.Execute(new List<string>() { "1" });

            writerMock.Verify(x => x.Clear(), Times.Once);
        }

        [Test]
        public void ExecuteShould_ThrowArgumentExceptionIfParametersAreNotProvided()
        {
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new BuyTicketsCommand(providerMock.Object, writerMock.Object);

            Assert.Throws<NullReferenceException>(() => command.Execute(null));
        }

        [Test]
        public void ExecuteShould_ReturnNoTicketsFoundIfIdDoesNotExistAndParameterIsProvided()
        {
            var ticketId = "1";
            var providerMock = new Mock<IPostgresDataProvider>();
            var writerMock = new Mock<IWriter>();
            providerMock.Setup(p => p.Tickets.Find(It.IsAny<Expression<Func<Ticket, bool>>>())).Returns(new Ticket[] { It.IsAny<Ticket>() });

            var command = new BuyTicketsCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { ticketId });

            Assert.That(result.Contains($"Sorry no tickets with this Id: {ticketId} were found!"));
        }
    }
}
