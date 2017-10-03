using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level2Workshop;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    class FakeConsoleInterface : IConsoleInterface
    {
        public Queue<string> UserInput;

        public string Output;

        public FakeConsoleInterface(IEnumerable<string> input)
        {
            UserInput = new Queue<string>(input);
            Output = "";
        }

        public string ReadLine()
        {
            if (UserInput.Count == 0)
            {
                return string.Empty;
            }

            return UserInput.Dequeue();
        }

        public void Write(object obj)
        {
            Output += obj.ToString();
        }

        public void Write(string format, object obj)
        {
            Output += string.Format(format, obj);
        }

        public void Write(string format, object[] args)
        {
            Output += string.Format(format, args);
        }
        
        public void WriteLine(object obj)
        {
            Output += obj.ToString() + "\r\n";
        }

        public void WriteLine(string format, object obj)
        {
            Output += string.Format(format, obj) + "\r\n";
        }

        public void WriteLine(string format, object[] args)
        {
            Output += string.Format(format, args) + "\r\n";
        }
    }
}
