using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Factories.Contracts;
using System.Linq.Expressions;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.Tests.ConsoleClient.Mocks;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class AddTournamentCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddPLayerCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddTournamentCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            Assert.IsInstanceOf<AddTournamentCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddTournamentCommand(null, writerMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddTournamentCommand(providerMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedFactoryIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new AddTournamentCommand(providerMock.Object, writerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddTournamentCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenNoParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddTournamentCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            string result = command.Execute(new List<string>());

            Assert.That(result.Contains("Not enough parameters!"));
        }

        //[Test]
        //[Ignore("Rewritten logic for method")]
        //public void ExecuteShould_CallCreateTournamentWhen4OrMoreParametersProvidedProvided()
        //{
        //    var surface = new SurfaceMock()
        //    {
        //        Id = 1,
        //        Type = "Grass",
        //        Speed = "Super Fast"
        //    };

        //    var providerMock = new Mock<ISqlServerDataProvider>();
        //    var writerMock = new Mock<IWriter>();
        //    var factoryMock = new Mock<IModelsFactory>();

        //    providerMock.Setup(p => p.Surfaces.Find(It.IsAny<Expression<Func<Surface, bool>>>())).Returns(new List<Surface>() { surface });

        //    var command = new AddTournamentCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

        //    try
        //    {
        //        command.Execute(new List<string>() { "Wimbledon", "Grass", "GS", "London" });
        //    }
        //    catch { }

        //    factoryMock.Verify(x => x.CreateTournament("Wimbledon", "", "", "", "GS", "", "London", "", "Grass", null), Times.Once);
        //}

        //[Test]
        //[Ignore("Rewritten logic for method")]
        //public void ExecuteShould_ThrowArgumentNullExceptionIfTournamentCannotBeCreated()
        //{
        //    var surface = new SurfaceMock()
        //    {
        //        Id = 1,
        //        Type = "Grass",
        //        Speed = "Super Fast"
        //    };

        //    var providerMock = new Mock<ISqlServerDataProvider>();
        //    var writerMock = new Mock<IWriter>();
        //    var factoryMock = new Mock<IModelsFactory>();

        //    providerMock.Setup(p => p.Surfaces.Find(It.IsAny<Expression<Func<Surface, bool>>>())).Returns(new List<Surface>() { surface });

        //    var command = new AddTournamentCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

        //    Assert.Throws<ArgumentNullException>(() => command.Execute(new List<string>() { "Wimbledon", "Grass", "GS", "London" }));
        //}
    }
}
