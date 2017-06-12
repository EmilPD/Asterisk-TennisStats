using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.SQLServerData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using ATPTennisStat.Tests.ConsoleClient.Mocks;
using ATPTennisStat.Models;
using System.Linq.Expressions;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class AddCountryCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddCountryCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            Assert.IsInstanceOf<AddCountryCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new AddCountryCommand(null, writerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();

            Assert.Throws<ArgumentNullException>(() => new AddCountryCommand(providerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x => x.Clear(), Times.Once);
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenNoParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>());

            Assert.That(result.Contains("Not enough parameters!"));
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenTooManyParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Mordor", "Europe", "Earth" });

            Assert.That(result.Contains("Not enough parameters!"));
        }

        [Test]
        public void ExecuteShould_ReturnCountryNameAlreadyExistsWhenCountryFoundInDB()
        {
            var countryName = "Mordor";
            var country = new CountryMock() { Id = 1, Name = countryName };

            var providerMock = new Mock<ISqlServerDataProvider>();

            providerMock.Setup(p => p.Countries.Find(It.IsAny<Expression<Func<Country, bool>>>())).Returns(new List<CountryMock>() { country });

            var writerMock = new Mock<IWriter>();

            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { countryName });

            Assert.That(result.Contains($"Country {countryName} already exists"));
        }

        [Test]
        public void ExecuteShould_ReturnCountrySuccessMessageWhenAddedToDB()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();

            providerMock.Setup(p => p.Countries.Find(It.IsAny<Expression<Func<Country, bool>>>())).Returns(new List<CountryMock>());
            providerMock.Setup(p => p.Countries.Add(It.IsAny<Country>())).Verifiable();
            providerMock.Setup(p => p.UnitOfWork.Finished()).Verifiable();

            var writerMock = new Mock<IWriter>();

            var command = new AddCountryCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Mordor" });

            Assert.That(result.Contains("Country Mordor was successfully added"));
        }
    }
}
