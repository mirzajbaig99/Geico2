using System;
using System.Diagnostics.CodeAnalysis;

namespace Level2Workshop
{
    [ExcludeFromCodeCoverage]
    public class ConsoleInterface : IConsoleInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(object obj)
        {
            Console.Write(obj);
        }

        public void Write(string format, object obj)
        {
            Write(string.Format(format, obj));
        }

        public void Write(string format, object[] args)
        {
            Write(string.Format(format, args));
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }

        public void WriteLine(string format, object obj)
        {
            WriteLine(string.Format(format, obj));
        }

        public void WriteLine(string format, object[] args)
        {
            WriteLine(string.Format(format, args));
        }
    }
}
