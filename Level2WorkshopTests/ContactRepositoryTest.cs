using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Level2Workshop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContactRepositoryTest
    {
        private ContactRepository repository;

        [ClassInitialize]
        public static void ConfigureMapper(TestContext context)
        {
            ContactMapperConfiguration.Configure();
        }

        [TestInitialize]
        public void Initialize()
        {
            repository = new ContactRepository();
        }

        [TestMethod]
        public void TestContactRepository_AddContact_WhenCompany_ShouldBeSuccessful()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();

            // Act
            repository.Add(contact);
            var contacts = repository.Get().ToList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.AreEqual(1, contacts.Count);
            Assert.IsTrue(contact.Equals(contacts[0]));
            Assert.AreEqual(contact, contacts[0]);
        }

        [TestMethod]
        public void TestContactRepository_AddContact_WhenFriend_ShouldBeSuccessful()
        {
            // Arrange
            var contact = ContactFactory.Get<FriendContact>();

            // Act
            repository.Add(contact);
            var contacts = repository.Get().ToList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.AreEqual(1, contacts.Count);
            Assert.IsTrue(contact.Equals(contacts[0]));
        }

        [TestMethod]
        public void TestContactRepository_AddContact_WhenWork_ShouldBeSuccessful()
        {
            // Arrange
            var contact = ContactFactory.Get<WorkContact>();

            // Act
            repository.Add(contact);

            // Assert
            var contacts = repository.Get().ToList();
            Assert.IsNotNull(contacts);
            Assert.AreEqual(1, contacts.Count);
            Assert.IsTrue(contact.Equals(contacts[0]));
        }

        [TestMethod]
        public void TestContactRepository_AddContact_WhenNull_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var condition = repository.Add((Contact)null);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestContactRepository_AddContact_WhenNameIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.Name = string.Empty;

            // Act
            var condition = repository.Add(contact);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestContactRepository_AddContact_WhenNameIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.Name = null;

            // Act
            var condition = repository.Add(contact);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestContactRepository_AddContact_WhenDuplicateTaxId_ShouldThrowException()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);

            // Act
            repository.Add(contact1);
            repository.Add(contact2);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestContactRepository_AddContact_WhenTaxIdIsNegative_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.TaxId = -1;

            // Act
            var condition = repository.Add(contact);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestContactRepository_AddContact_WhenTaxIdIsTooLarge_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.TaxId = 1000000000;

            // Act
            var condition = repository.Add(contact);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestContactRepository_AddContact_WhenTaxIdIsZero_ShouldThrowArgumentException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.TaxId = 0;

            // Act
            var condition = repository.Add(contact);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TestContactRepository_AddContact_WhenNotDuplicateTaxId_ShouldBeSuccessful()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.TaxId++;

            // Act
            bool condition = repository.Add(contact1);
            condition &= repository.Add(contact2);

            // Assert
            Assert.IsTrue(condition);

            var contacts = repository.Get().ToList();
            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual(contact1.TaxId, contacts[0].TaxId);
            Assert.AreEqual(contact2.TaxId, contacts[1].TaxId);
        }

        [TestMethod]
        public void TestContactRepository_AddContactCollection_WhenNotDuplicateTaxId_ShouldBeSuccessful()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.TaxId++;

            var contactList = new List<Contact>
                              {
                                  contact1,
                                  contact2
                              };

            // Act
            bool condition = repository.Add(contactList);

            // Assert
            Assert.IsTrue(condition);

            var contacts = repository.Get().ToList();
            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual(contact1.TaxId, contacts[0].TaxId);
            Assert.AreEqual(contact2.TaxId, contacts[1].TaxId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestContactRepository_AddContactCollection_WhenDuplicateTaxId_ShouldThrowException()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);

            var contactList = new List<Contact>
                              {
                                  contact1,
                                  contact2
                              };

            // Act
            bool condition = repository.Add(contactList);

            // Assert
        }

        [TestMethod]
        public void TestContactRepository_GetAllContacts_WhenPopulated_ShouldBeSuccessful()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.TaxId++;

            repository.Add(contact1);
            repository.Add(contact2);

            // Act
            var contacts = repository.Get().ToList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.AreEqual(2, contacts.Count);
        }

        [TestMethod]
        public void TestContactRepository_GetAllContacts_WhenEmpty_ShouldBeSuccessful()
        {
            // Arrange
            // Act
            var contacts = repository.Get().ToList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        public void TestContactRepository_GetContactById_WhenPresent_ShouldBeSuccessful()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.TaxId++;

            repository.Add(contact1);
            repository.Add(contact2);

            // Act
            var contact = repository.Get(contact1.TaxId);

            // Assert
            Assert.IsNotNull(contact);
            Assert.IsTrue(contact1.Equals(contact));
        }

        [TestMethod]
        public void TestContactRepository_GetContactById_WhenNotPresent_ShouldReturnNull()
        {
            // Arrange
            // Act
            var contact = repository.Get(999);

            // Assert
            Assert.IsNull(contact);
        }

        [TestMethod]
        public void TestContactRepository_GetContactByName_WhenPresent_ShouldBeSuccessful()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.Name += "2";
            contact2.TaxId++;

            repository.Add(contact1);
            repository.Add(contact2);

            // Act
            var contact = repository.Get(contact1.Name);

            // Assert
            Assert.IsNotNull(contact);
            Assert.IsTrue(contact1.Equals(contact));
        }

        [TestMethod]
        public void TestContactRepository_GetContactByName_WhenNotPresent_ShouldReturnNull()
        {
            // Arrange
            // Act
            var contact = repository.Get("not there");

            // Assert
            Assert.IsNull(contact);
        }

        [TestMethod]
        public void TestContactRepository_UpdateContact_WhenPresentWithNewTaxId_ShouldReturnTrue()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.Name += "2";
            contact2.TaxId++;

            repository.Add(contact1);

            // Act
            var condition = repository.Update(contact2, contact1.TaxId);

            // Assert
            Assert.IsTrue(condition);
            Assert.IsTrue(contact2.Equals(repository.Get(contact2.TaxId)));
            Assert.AreEqual(1, repository.Get().Count());
        }

        [TestMethod]
        public void TestContactRepository_UpdateContact_WhenNull_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            repository.Add(contact1);

            // Act
            var condition = repository.Update(null, contact1.TaxId);

            // Assert
            Assert.IsFalse(condition);
            Assert.IsTrue(contact1.Equals(repository.Get(contact1.TaxId)));
        }

        [TestMethod]
        public void TestContactRepository_UpdateContact_WhenNotPresent_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            repository.Add(contact1);
            int badTaxId = contact1.TaxId + 1;

            // Act
            var condition = repository.Update(contact1, badTaxId);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TestContactRepository_UpdateContact_WhenTaxIdChangeDuplicate_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.Name += "2";
            contact2.TaxId++;

            repository.Add(contact1);
            repository.Add(contact2);

            // Act
            var contact = repository.Get(contact1.Name);

            // Assert
            Assert.IsNotNull(contact);
            Assert.IsTrue(contact1.Equals(contact));
        }

        [TestMethod]
        public void TestContactRepository_RemoveContact_WhenPresent_ShouldReturnTrue()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.Name += "2";
            contact2.TaxId++;

            repository.Add(contact1);
            repository.Add(contact2);

            // Act
            var condition = repository.Remove(contact1);

            // Assert
            var contacts = repository.Get().ToList();
            Assert.IsTrue(condition);
            Assert.AreEqual(1, contacts.Count);
            Assert.AreEqual(contact2, contacts[0]);
        }

        [TestMethod]
        public void TestContactRepository_RemoveContact_WhenNotPresent_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            var contact2 = ContactFactory.Get<CompanyContact>(ContactDataType.Minimum);
            contact2.Name += "2";
            contact2.TaxId++;

            repository.Add(contact1);

            // Act
            var condition = repository.Remove(contact2);

            // Assert
            var contacts = repository.Get().ToList();

            Assert.IsFalse(condition);
            Assert.AreEqual(1, contacts.Count);
            Assert.AreEqual(contact1, contacts[0]);
        }

        [TestMethod]
        public void TestContactRepository_RemoveContact_WhenNull_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var condition = repository.Remove(null);

            // Assert
            Assert.IsFalse(condition);
        }
    }
}
