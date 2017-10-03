using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Level2Workshop;
using Level2Workshop.Model;
using Level2Workshop.Repositories;
using Level2Workshop.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContactRespositoryTest
    {

        [TestMethod]
        public void RepositoryTest_Add_Success()
        {
            //Arrange
            var contactsDto = new List<ContactDto>();
            var repository = new ContactRepository(contactsDto);
            var dateTime = DateTime.Now;
            WorkContact wc = new WorkContact() { TaxId = 1,
                                            Name = "Name",
                                            Address = "address1",
                                            Company = "Company",
                                            DateModified = dateTime,
                                            EmailAddress = "email@gmail.com",
                                            PhoneNumber = "000000000",
                                            Title = "Title",
                                            Url = "www.url.com"
                                          };
            //Act
            repository.Add(wc);
           
            //Assert
            Assert.AreEqual(contactsDto.Count,1);
            Assert.IsInstanceOfType(contactsDto[0], typeof(WorkDto));
            Assert.AreEqual(contactsDto[0].Address, wc.Address);
            Assert.AreEqual(contactsDto[0].DateCreated, wc.DateCreated);
            Assert.AreEqual(contactsDto[0].DateModified, wc.DateModified);
            Assert.AreEqual(contactsDto[0].Name, wc.Name);
            Assert.AreEqual(contactsDto[0].PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(contactsDto[0].TaxId, wc.TaxId);
            Assert.AreEqual(((WorkDto)contactsDto[0]).Title, wc.Title);
            Assert.AreEqual(((WorkDto)contactsDto[0]).Url, wc.Url);
            
        }

        [TestMethod]
        public void RepositoryTest_Update_Work_Success()
        {
            //Arrange
            var dateTime = DateTime.Now;
            WorkContact wc = ((WorkContact)FakeDataFactory.Get<WorkContact>(
                                taxId:1,
                                name:"Name1",
                                address:"address11",
                                company:"Company1",
                                dateModified:dateTime,
                                emailAddress:"email@gmail.com1",
                                phoneNumber:"000000001",
                                title:"Title1",
                                url:"www.url1.com"
                        ));
            var contactsDto = new List<ContactDto>()
                {
                    FakeDataFactory.GetDto<WorkDto>()
                };
            var repository = new ContactRepository(contactsDto);
          
            //Act
            repository.Update(wc, 1);

            //Assert
            Assert.AreEqual(contactsDto.Count, 1);
            Assert.IsInstanceOfType(contactsDto[0], typeof(WorkDto));
            Assert.AreEqual(contactsDto[0].Address, wc.Address);
            Assert.AreEqual(contactsDto[0].Name, wc.Name);
            Assert.AreEqual(contactsDto[0].PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(contactsDto[0].TaxId, wc.TaxId);
            Assert.AreEqual(((WorkDto)contactsDto[0]).Title, wc.Title);
            Assert.AreEqual(((WorkDto)contactsDto[0]).Url, wc.Url);

        }

        [TestMethod]
        public void RepositoryTest_Update_Company_Success()
        {
            //Arrange
            var dateTime = DateTime.Now;
            CompanyContact wc = ((CompanyContact)FakeDataFactory.Get<CompanyContact>(
                                taxId: 1,
                                name: "Name1",
                                address: "address11",
                                phoneNumber: "000000001",
                                url: "www.url1.com"
                        ));
            var contactsDto = new List<ContactDto>()
                {
                    FakeDataFactory.GetDto<CompanyDto>()
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            repository.Update(wc, 1);

            //Assert
            Assert.AreEqual(contactsDto.Count, 1);
            Assert.IsInstanceOfType(contactsDto[0], typeof(CompanyDto));
            Assert.AreEqual(contactsDto[0].Address, wc.Address);
            Assert.AreEqual(contactsDto[0].Name, wc.Name);
            Assert.AreEqual(contactsDto[0].PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(contactsDto[0].TaxId, wc.TaxId);
            Assert.AreEqual(((CompanyDto)contactsDto[0]).Url, wc.Url);
            

        }

        [TestMethod]
        public void RepositoryTest_Update_Friend_Success()
        {
            //Arrange
            var dateTime = DateTime.Now;
            FriendContact wc = ((FriendContact)FakeDataFactory.Get<FriendContact>(
                                taxId: 10,
                                name: "Name1",
                                address: "address11",
                                company: "Company1",
                                dateModified: dateTime,
                                emailAddress: "email@gmail.com1",
                                phoneNumber: "000000001",
                                birthday:"01/01/2000"
                        ));
            var contactsDto = new List<ContactDto>()
                {
                    FakeDataFactory.GetDto<FriendDto>()
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            repository.Update(wc, 1);

            //Assert
            Assert.AreEqual(contactsDto.Count, 1);
            Assert.IsInstanceOfType(contactsDto[0], typeof(FriendDto));
            Assert.AreEqual(contactsDto[0].Address, wc.Address);
            Assert.AreEqual(contactsDto[0].Name, wc.Name);
            Assert.AreEqual(contactsDto[0].PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(contactsDto[0].TaxId, wc.TaxId);
            Assert.AreEqual(((FriendDto)contactsDto[0]).Birthday, wc.Birthday);
       }

        [TestMethod]
        public void RepositoryTest_Update_NotFound_Fail()
        {
            //Arrange
            var dateTime = DateTime.Now;
            WorkDto wDto = ((WorkDto)FakeDataFactory.GetDto<WorkDto>(
                                taxId: 1,
                                name: "Name1",
                                address: "address11",
                                company: "Company1",
                                dateModified: dateTime,
                                emailAddress: "email@gmail.com1",
                                phoneNumber: "000000001",
                                title: "Title1",
                                url: "www.url1.com"
                        ));
            var contactsDto = new List<ContactDto>()
                {
                    wDto
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.Update(FakeDataFactory.Get<WorkContact>(), 10);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RepositoryTest_Update_TypeMisMatch_Fail()
        {
            //Arrange
            var dateTime = DateTime.Now;
            WorkDto wDto = ((WorkDto)FakeDataFactory.GetDto<WorkDto>(
                                taxId: 1,
                                name: "Name1",
                                address: "address11",
                                company: "Company1",
                                dateModified: dateTime,
                                emailAddress: "email@gmail.com1",
                                phoneNumber: "000000001",
                                title: "Title1",
                                url: "www.url1.com"
                        ));
            var contactsDto = new List<ContactDto>()
                {
                    wDto
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.Update(FakeDataFactory.Get<FriendContact>(), 1);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RepositoryTest_Update_TaxIdUnique_Fail()
        {
            //Arrange
            var dateTime = DateTime.Now;
            WorkDto wDto = ((WorkDto)FakeDataFactory.GetDto<WorkDto>(
                                taxId: 2
                        ));
            var contactsDto = new List<ContactDto>()
                {
                        FakeDataFactory.GetDto<WorkDto>(
                                taxId: 1
                                ),
                        wDto
                        
                };
            var repository = new ContactRepository(contactsDto);
            var updatedContact = wDto.ToContact();
            updatedContact.TaxId = 1;

            //Act
            var result = repository.Update(updatedContact, 2);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RepositoryTest_AddNull_Fail()
        {
            //Arrange
            var contactsDto = new List<ContactDto>();
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.Add(null);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RepositoryTest_AddContainsKey_Fail()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                new WorkDto() { TaxId = 1,
                                            Name = "Name",
                                            Address = "address1",
                                            Company = "Company",
                                            EmailAddress = "email@gmail.com",
                                            PhoneNumber = "000000000",
                                            Title = "Title",
                                            Url = "www.url.com"
                                          }
            };
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.Add(new WorkContact { TaxId = 1});

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RepositoryTest_Remove_Success()
        {
            //Arrange
            ContactDto contact = FakeDataFactory.GetDto<WorkDto>();

            var contactsDto = new List<ContactDto>() {
                      contact
                };
            var repository = new ContactRepository(contactsDto);
          
            //Act
            bool result = repository.Remove(contact.ToContact());

            //Assert
            Assert.AreEqual(contactsDto.Count, 0);
           
        }

        [TestMethod]
        public void RepositoryTest_RemoveNotFound_Fail()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            bool result = repository.Remove(new WorkContact() { TaxId = 1 });

            //Assert
            Assert.AreEqual(contactsDto.Count, 0);

        }

        [TestMethod]
        public void RepositoryTest_Remove_NullItem_Fail()
        {
            //Arrange
            var contactsDto = new List<ContactDto>()
            {
            };
            var repository = new ContactRepository(contactsDto);

            //Act
            bool result = repository.Remove(null);

            //Assert
            Assert.AreEqual(contactsDto.Count, 0);

        }

        [TestMethod]
        public void RepositoryTest_GetById_Success()
        {
            //Arrange
            WorkDto wc = (WorkDto) FakeDataFactory.GetDto<WorkDto>();
            var contactsDto = new List<ContactDto>() {
                      wc
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            Contact result = repository.Get(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(WorkContact));
            Assert.AreEqual(result.Address, wc.Address);
            Assert.AreEqual(result.DateCreated, wc.DateCreated);
            Assert.AreEqual(result.DateModified, wc.DateModified);
            Assert.AreEqual(result.Name, wc.Name);
            Assert.AreEqual(result.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(result.TaxId, wc.TaxId);
            Assert.AreEqual(((WorkContact)result).Title, wc.Title);
            Assert.AreEqual(((WorkContact)result).Url, wc.Url);

        }

        [TestMethod]
        public void RepositoryTest_GetById_NotFound()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            Contact result = repository.Get(1);

            //Assert
            Assert.IsNull(result);
         }

        [TestMethod]
        public void RepositoryTest_GetByName_NotFound()
        {
            //Arrange
            var contactsDto = new List<ContactDto>()
            {
            };
            var repository = new ContactRepository(contactsDto);

            //Act
            Contact result = repository.Get("Name");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RepositoryTest_GetByName()
        {
            //Arrange
            WorkDto wc = new WorkDto()
            {
                TaxId = 1,
                Name = "Name",
                Address = "address1",
                Company = "Company",
                EmailAddress = "email@gmail.com",
                PhoneNumber = "000000000",
                Title = "Title",
                Url = "www.url.com"
            };
            var contactsDto = new List<ContactDto>() {
                      wc
                };
            var repository = new ContactRepository(contactsDto);

            //Act
            Contact result = repository.Get("Name");

            //Assert
            Assert.IsInstanceOfType(result, typeof(WorkContact));
            Assert.AreEqual(result.Address, wc.Address);
            Assert.AreEqual(result.DateCreated, wc.DateCreated);
            Assert.AreEqual(result.DateModified, wc.DateModified);
            Assert.AreEqual(result.Name, wc.Name);
            Assert.AreEqual(result.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(result.TaxId, wc.TaxId);
            Assert.AreEqual(((WorkContact)result).Title, wc.Title);
            Assert.AreEqual(((WorkContact)result).Url, wc.Url);

        }

        [TestMethod]
        public void RepositoryTest_GetAll()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                      new WorkDto()
                        {
                            TaxId = 1,
                            Name = "Name",
                            Address = "address1",
                            Company = "Company",
                            EmailAddress = "email@gmail.com",
                            PhoneNumber = "000000000",
                            Title = "Title",
                            Url = "www.url.com"
                        },
                       new CompanyDto()
                        {
                            TaxId = 2,
                            Name = "Name",
                            Address = "address1",
                            PhoneNumber = "000000000",
                            Url = "www.url.com"
                        }

                    };
            var repository = new ContactRepository(contactsDto);

            //Act
            IEnumerable<Contact> result = repository.Get();

            //Assert
            Assert.AreEqual(result.Count(),2);
            
        }

        [TestMethod]
        public void RepositoryTest_ContainsKeySuccess()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                      new WorkDto()
                        {
                            TaxId = 1,
                            Name = "Name",
                            Address = "address1",
                            Company = "Company",
                            EmailAddress = "email@gmail.com",
                            PhoneNumber = "000000000",
                            Title = "Title",
                            Url = "www.url.com"
                        },
                    };
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.ContainsKey(1);

            //Assert
            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public void RepositoryTest_ContainsKeyFalse()
        {
            //Arrange
            var contactsDto = new List<ContactDto>() {
                      new WorkDto()
                        {
                            TaxId = 1,
                            Name = "Name",
                            Address = "address1",
                            Company = "Company",
                            EmailAddress = "email@gmail.com",
                            PhoneNumber = "000000000",
                            Title = "Title",
                            Url = "www.url.com"
                        },
                    };
            var repository = new ContactRepository(contactsDto);

            //Act
            var result = repository.ContainsKey(2);

            //Assert
            Assert.AreEqual(result, false);

        }


    }
}
