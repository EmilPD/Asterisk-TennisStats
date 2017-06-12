using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLServerData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new BuyTicketCommand(providerMock.Object, writerMock.Object);

            Assert.IsInstanceOf<BuyTicketCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new BuyTicketCommand(null, writerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();

            Assert.Throws<ArgumentNullException>(() => new BuyTicketCommand(providerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new BuyTicketCommand(providerMock.Object, writerMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }
    }
}
