using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using ATPTennisStat.Tests.ConsoleClient.Mocks;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Factories.Contracts;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class AddPlayerCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddPLayerCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddPlayerCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            Assert.IsInstanceOf<AddPlayerCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddPlayerCommand(null, writerMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddPlayerCommand(providerMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedFactoryIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new AddPlayerCommand(providerMock.Object, writerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddPlayerCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }
    }
}
