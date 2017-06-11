using System;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using Bytes2you.Validation;
using ATPTennisStat.ConsoleClient.Core.Utilities;

namespace ATPTennisStat.ConsoleClient.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ILogger logger;
        private IParser parser;

        public Engine(IReader reader, IWriter writer, ILogger logger, IParser parser)
        {
            this.Reader = reader;
            this.Writer = writer;
            this.Logger = logger;
            this.Parser = parser;
        }

        public IReader Reader
        {
            get
            {
                return this.reader;
            }

            set
            {
                Guard.WhenArgument(value, "Engine Reader provider").IsNull().Throw();
                this.reader = value;
            }
        }

        public IWriter Writer
        {
            get
            {
                return this.writer;
            }

            set
            {
                Guard.WhenArgument(value, "Engine Writer provider").IsNull().Throw();
                this.writer = value;
            }
        }

        public ILogger Logger
        {
            get
            {
                return this.logger;
            }

            set
            {
                Guard.WhenArgument(value, "Engine Logger provider").IsNull().Throw();
                this.logger = value;
            }
        }

        public IParser Parser
        {
            get
            {
                return this.parser;
            }

            set
            {
                Guard.WhenArgument(value, "Engine command parser provider").IsNull().Throw();
                this.parser = value;
            }
        }

        public void Start()
        {
            this.writer.WriteLine(Messages.GenerateWelcomeMessage());
            this.writer.WriteLine(Messages.GenerateMainMenu());

            var builder = new StringBuilder();

            while(true)
            {
                var commandLine = this.reader.ReadLine();

                if (commandLine.ToLower() == "exit")
                {
                    this.writer.Write(builder.ToString());
                    this.writer.WriteLine("Program terminated.");
                    break;
                }

                try
                {
                    var executionResult = this.parser.ParseCommand(commandLine);
                    this.writer.WriteLine(executionResult);
                }
                catch (ArgumentException ex)
                {
                    builder.AppendLine(ex.Message);
                    //need to re-add menu footer
                    this.logger.Log(ex.Message);
                    //this.writer.Write(ex.Message) to refactor logger
                }
                catch (Exception ex)
                {
                    builder.AppendLine("Opps, something happened. :(");
                    this.logger.Log(ex.Message);
                }
            }
        }
    }
}