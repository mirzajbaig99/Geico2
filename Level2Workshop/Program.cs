using System.Diagnostics.CodeAnalysis;

namespace Level2Workshop
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static ContactsUi contactsUi;

        static void Main(string[] args)
        {
            contactsUi = new ContactsUi();

            contactsUi.Run();
        }
    }
}
