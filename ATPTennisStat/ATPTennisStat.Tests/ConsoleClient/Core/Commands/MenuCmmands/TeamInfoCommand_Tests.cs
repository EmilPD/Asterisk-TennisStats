using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class TeamInfoCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfTeamInfoCommandClass_WhenThePassedValuesAreValid()
        {
            var writerMock = new Mock<IWriter>();
            var command = new TeamInfoCommand(writerMock.Object);

            Assert.IsInstanceOf<TeamInfoCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TeamInfoCommand(null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var writerMock = new Mock<IWriter>();
            var command = new TeamInfoCommand(writerMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }
    }
}
