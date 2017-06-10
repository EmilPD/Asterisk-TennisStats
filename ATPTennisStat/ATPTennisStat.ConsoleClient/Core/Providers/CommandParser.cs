using ATPTennisStat.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bytes2you.Validation;
using ATPTennisStat.ConsoleClient.Core.Factories;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class CommandParser : IParser
    {
        private ICommandFactory factory;

        public CommandParser(ICommandFactory factory)
        {
            this.factory = factory;
        }

        public ICommandFactory CommandsFactory
        {
            get
            {
                return this.factory;
            }

            set
            {
                Guard.WhenArgument(value, "CommandParser CommandsFactory").IsNull().Throw();
                this.factory = value;
            }
        }

        public string ParseCommand(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
            {
                throw new ArgumentException(nameof(commandLine));
            }

            var commandName = commandLine.Split(' ')[0];
            var commandParameters = commandLine
                .Split(' ')
                .Skip(1)
                .ToList();

            var command = this.CommandsFactory.CreateCommandFromString(commandName);
            var executionResult = command.Execute(commandParameters);

            return executionResult;
        }
    }
}