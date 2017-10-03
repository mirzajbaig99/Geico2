using System;
using System.Diagnostics.CodeAnalysis;
using Level2Workshop;

namespace Level2WorkshopTests
{
    [ExcludeFromCodeCoverage]
    public static class ContactFactory
    {
        public static T Get<T>(ContactDataType dataType = ContactDataType.Basic) where T : Contact
        {
            Contact contact = Activator.CreateInstance<T>();

            switch (dataType)
            {
                case ContactDataType.Empty:
                    break;

                case ContactDataType.Minimum:
                    SetMinimumData(contact);
                    break;

                case ContactDataType.Basic:
                    SetBasicData(contact);
                    break;

                case ContactDataType.Company:
                    SetCompanyData(contact);
                    break;

                case ContactDataType.Friend:
                    SetFriendData(contact);
                    break;

                case ContactDataType.Work:
                    SetWorkData(contact);
                    break;
            }
            return contact as T;
        }

        private static void SetMinimumData(Contact contact)
        {
            contact.Name = "name";
            contact.TaxId = 1;
        }

        private static void SetBasicData(Contact contact)
        {
            contact.Name = "basic name";
            contact.TaxId = 2;
            contact.Address = "basic address";
            contact.PhoneNumber = "123-456-7890";
            contact.DateCreated = DateTime.Now;
        }

        private static void SetCompanyData(Contact contact)
        {
            if (contact is CompanyContact)
            {
                var company = contact as CompanyContact;

                company.Name = "company name";
                company.TaxId = 3;
                company.Address = "company address";
                company.PhoneNumber = "123-456-7890";
                company.DateCreated = DateTime.Now;

                company.Url = "company url";
            }
        }

        private static void SetFriendData(Contact contact)
        {
            if (contact is FriendContact)
            {
                var friend = contact as FriendContact;

                friend.Name = "friend name";
                friend.TaxId = 3;
                friend.Address = "friend address";
                friend.PhoneNumber = "123-456-7890";
                friend.DateCreated = DateTime.Now;

                friend.Birthday = "birthday";
                friend.EmailAddress = "friend email";
            }
        }

        private static void SetWorkData(Contact contact)
        {
            if (contact is WorkContact)
            {
                var work = contact as WorkContact;

                work.Name = "work name";
                work.TaxId = 3;
                work.Address = "work address";
                work.PhoneNumber = "123-456-7890";
                work.DateCreated = DateTime.Now;

                work.Company = "company";
                work.EmailAddress = "work email";
                work.Title = "title";
                work.Url = "work url";
            }
        }
    }
}
