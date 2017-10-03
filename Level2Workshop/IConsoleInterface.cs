namespace Level2Workshop
{
    public interface IConsoleInterface
    {
        string ReadLine();
        void Write(string format, object obj);
        void Write(string format, object[] args);
        void Write(object obj);
        void WriteLine(string format, object obj);
        void WriteLine(string format, object[] args);
        void WriteLine(object obj);
    }
}
