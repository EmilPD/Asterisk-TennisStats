﻿using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ImportCommands
{
    public class ImportSampleDataCommand : ICommand
    {
        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;
        private IExcelImporter excelImporter;
        private IWriter writer;
        private ILogger logger;

        public ImportSampleDataCommand(ISqlServerDataProvider dataProvider,
                                       IModelsFactory modelsFactory,
                                       IExcelImporter excelImporter,
                                       IWriter writer,
                                       ILogger logger)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;
            this.excelImporter = excelImporter;
            this.writer = writer;
            this.logger = logger;

        }

        public string Execute(IList<string> parameters)
        {
            var players = excelImporter.ImportPlayers();
            //excelImporter.ImportTournaments();
            //excelImporter.ImportPointDistributions();
            //excelImporter.ImportMatches();

            return "";
        }
    }
}
