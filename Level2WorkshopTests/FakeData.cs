using System.Diagnostics.CodeAnalysis;

namespace Level2WorkshopTests
{
    [ExcludeFromCodeCoverage]
    public static class FakeData
    {
        public static string[] GetBasicContactData()
        {
            return new[]
                {
                    "testname", 
                    "1", 
                    "201-111-1111", 
                    "123 main st anywhere usa"
                };
        }

        public static string[] GetFriendContactData()
        {
            return new[]
                {
                    "testname3", 
                    "3", 
                    "201-111-1111", 
                    "123 main st anywhere usa",
                    "test@noreply.com",
                    "1/2/90"
                };
        }

        public static string[] GetWorkContactData()
        {
            return new[]
                {
                    "testname4", 
                    "4", 
                    "201-111-1111",
                    "123 main st anywhere usa",
                    "manager",
                    "fakeCo",
                    "test@noreply.com",
                    "www.mycompany.com"
                };
        }

        public static string[] GetMinimumContactData()
        {
            return new[]
                {
                    "testname2", 
                    "2",
                    "", 
                    ""
                };
        }

        public static string[] GetCompanyContactData()
        {
            return new[]
                {
                    "testname", 
                    "1", 
                    "201-111-1111", 
                    "123 Main St Anywhere USA",
                    "www.mycompany.com"
                };
        }

        public static string[] GetEmptyContactData()
        {
            return new[] { string.Empty };
        }
    }
}
