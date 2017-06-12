using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Tests.ConsoleClient.Mocks;

namespace ATPTennisStat.Tests.ConsoleClient.Core.Commands.DataCommands.DataAddCommands
{
    [TestFixture]
    class AddCityCommand_Tests
    {
        [Test]
        public void ConstructorShould_ReturnInstanceOfAddCityCommandClass_WhenThePassedValuesAreValid()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);
            
            Assert.IsInstanceOf<AddCityCommand>(command);
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedProviderIsNull()
        {
            var writerMock = new Mock<IWriter>();

            Assert.Throws<ArgumentNullException>(() => new AddCityCommand(null, writerMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenThePassedWriterIsNull()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();

            Assert.Throws<ArgumentNullException>(() => new AddCityCommand(providerMock.Object, null));
        }

        [Test]
        public void ExecuteShould_ClearTheScreenOneTIme()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            command.Execute(new List<string>());

            writerMock.Verify(x =>x.Clear(), Times.Once);
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenNoParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Sofia" });

            Assert.That(result.Contains("Not enough parameters!"));
        }

        [Test]
        public void ExecuteShould_ReturnNotEnoughParametersWhenTooManyParametersProvided()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Varna", "Bulgaria", "Europe", "Earth" });

            Assert.That(result.Contains("Not enough parameters!"));
        }

        [Test]
        public void ExecuteShould_ReturnNoCountryNameExistsWhenCountryNotFoundInDB()
        {
            var providerMock = new Mock<ISqlServerDataProvider>();
            providerMock.Setup(m => m.Countries.Equals(new List<CountryMock>() { null }));

            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Varna", "Mordor" });

            Assert.That(result.Contains("No country Mordor exists"));
        }

        [Test]
        public void ExecuteShould_ReturnCityNameNameAlreadyExistsWhenCityFoundInDB()
        {
            var countryName = "Mordor";
            var country = new CountryMock() { Id = 1, Name = countryName };
            var cityName = "Varna";
            var city = new CityMock() { Id = 1, Name = cityName, Country = country };

            var providerMock = new Mock<ISqlServerDataProvider>();

            providerMock.Setup(p => p.Countries.Find(It.IsAny<Expression<Func<Country, bool>>>())).Returns(new List<CountryMock>() { country });

            providerMock.Setup(p => p.Cities.Find(It.IsAny<Expression<Func<City, bool>>>())).Returns(new List<CityMock>() { city });

            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { cityName, countryName });

            Assert.That(result.Contains($"City {cityName}, {countryName} already exists"));
        }

        [Test]
        public void ExecuteShould_ReturnCitySuccessfullyAddedIfCityIsAddedToDB()
        {
            var countryName = "Mordor";
            var country = new CountryMock() { Id = 1, Name = countryName };
            var cityName = "Varna";
            var city = new CityMock() { Id = 1, Name = cityName, Country = country };

            var providerMock = new Mock<ISqlServerDataProvider>();

            providerMock.Setup(p => p.Countries.Find(It.IsAny<Expression<Func<Country, bool>>>())).Returns(new List<CountryMock>() { country });

            providerMock.Setup(p => p.Cities.Find(It.IsAny<Expression<Func<City, bool>>>())).Returns(new List<CityMock>() { null });

            providerMock.Setup(p => p.Cities.Add(It.IsAny<City>())).Verifiable();
            providerMock.Setup(p => p.UnitOfWork.Finished()).Verifiable();

            var writerMock = new Mock<IWriter>();
            var command = new AddCityCommand(providerMock.Object, writerMock.Object);

            string result = command.Execute(new List<string>() { "Plovdiv", countryName });

            Assert.That(result.Contains($"City Plovdiv was successfully added"));
        }
    }
}