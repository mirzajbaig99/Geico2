using System.Diagnostics.CodeAnalysis;
using Level2Workshop;

namespace Level2WorkshopTests
{
    [ExcludeFromCodeCoverage]
    public static class DataFactory
    {
        public static string[] GetDataAsStringArray(ContactDataType contactData)
        {
            switch (contactData)
            {
                case ContactDataType.Empty:
                    return FakeData.GetEmptyContactData();

                case ContactDataType.Basic:
                    return FakeData.GetBasicContactData();

                case ContactDataType.Friend:
                    return FakeData.GetFriendContactData();

                case ContactDataType.Work:
                    return FakeData.GetWorkContactData();

                case ContactDataType.Company:
                    return FakeData.GetCompanyContactData();

                default:
                    return FakeData.GetMinimumContactData();
            }
        }
    }
}