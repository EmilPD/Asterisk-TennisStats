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
    class AddMatchCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddMatchCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();
            var command = new AddMatchCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            Assert.IsInstanceOf<AddMatchCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddMatchCommand(null, writerMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var factoryMock = new Mock<IModelsFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddMatchCommand(providerMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedFactoryIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new AddMatchCommand(providerMock.Object, writerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddMatchCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenNoParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();

            var command = new AddMatchCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            string result = command.Execute(new List<string>());

            Assert.That(result.Contains("Not enough parameters!"));
        }

        [Test]
        public void ExecuteShould_CallCreateMatchWith6ArgumentsIfTooManyParametersProvided()
        {
            var par = new List<string>() { "2017-06-05", "Ivo", "Karlovic", "Novak", "Jocovic", "3-2", "3", "R64", "something", "else" };
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();
            
            var testTournament = new Tournament()
            {
                Name = "test",
                StartDate = new DateTime(),
                EndDate = new DateTime(),
                PrizeMoney = 1234.5m,
                Category = new TournamentCategoryMock(),
                City = new CityMock(),
                Type = new SurfaceMock()
            };

            providerMock.Setup(p => p.Tournaments.Get(It.IsAny<int>())).Returns(testTournament);

            var command = new AddMatchCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            try
            {
                var result = command.Execute(par);
            }
            catch
            { }

            factoryMock.Verify(x => x.CreateMatch(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ExecuteShould_ThrowArgumentNullExceptionIfCreatedMatchIsNull()
        {
            var par = new List<string>() { "2017-06-05", "Ivo", "Karlovic", "Novak", "Jocovic", "3-2", "3", "R64", "something", "else" };
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var factoryMock = new Mock<IModelsFactory>();
            
            var testTournament = new TournamentMock()
            {
                Name = "test",
                StartDate = new DateTime(),
                EndDate = new DateTime(),
                PrizeMoney = 1234.5m,
                Category = new TournamentCategoryMock(),
                City = new CityMock(),
                Type = new SurfaceMock()
            };

            providerMock.Setup(p => p.Tournaments.Get(It.IsAny<int>())).Returns(testTournament);

            var command = new AddMatchCommand(providerMock.Object, writerMock.Object, factoryMock.Object);

            Assert.Throws<ArgumentNullException>(() => command.Execute(par));
        }
    }
}
