using ATPTennisStat.ConsoleClient.Core.Contracts;
using System;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}