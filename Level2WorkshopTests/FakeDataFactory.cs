using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Level2Workshop;
using Level2Workshop.Model;

namespace Level2WorkshopTests
{
    [ExcludeFromCodeCoverage]
    public class FakeDataFactory 
    {
        public static Contact Get<T>(
                int taxId = 1,
                string name = "Name",
                string address = "address1",
                string company = "Company",
                string emailAddress = "email@gmail.com",
                string phoneNumber = "000000000",
                string title = "Title",
                string url = "www.url.com",
                string birthday = "01/01/1999",
                DateTime? dateModified = null
            ) where T:Contact
        {
            if (typeof(T) == typeof(WorkContact))
            {
                return new WorkContact()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    Company = company,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Title = title,
                    Url = url,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }
            else if (typeof(T) == typeof(CompanyContact))
            {
                return new CompanyContact()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Url = url,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }
            else if (typeof(T) == typeof(FriendContact))
            {
                return new FriendContact()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Birthday = birthday,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }

            return null;
        }

        public static ContactDto GetDto<T>(
                int taxId = 1,
                string name = "Name",
                string address = "address1",
                string company = "Company",
                string emailAddress = "email@gmail.com",
                string phoneNumber = "000000000",
                string title = "Title",
                string url = "www.url.com",
                string birthday = "01/01/1999",
                DateTime? dateModified = null) where T : ContactDto
        {
            if (typeof(T) == typeof(WorkDto))
            {
                return new WorkDto()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    Company = company,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Title = title,
                    Url = url,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }
            else if (typeof(T) == typeof(CompanyDto))
            {
                return new CompanyDto()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Url = url,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }
            else if (typeof(T) == typeof(FriendDto))
            {
                return new FriendDto()
                {
                    TaxId = taxId,
                    Name = name,
                    Address = address,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Birthday = birthday,
                    DateModified = dateModified ?? DateTime.MinValue
                };
            }

            return null;
        }


    }
}
