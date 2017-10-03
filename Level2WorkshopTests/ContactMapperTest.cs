using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Level2Workshop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContactMapperTest
    {
        private ContactMapper contactMapper;

        [ClassInitialize]
        public static void InitializeMapperConfiguration(TestContext context)
        {
            ContactMapperConfiguration.Configure();
        }

        [TestInitialize]
        public void Initialize()
        {
            contactMapper = new ContactMapper();
        }

        [TestMethod]
        public void TestContactMapper_MapToContact_WhenFriendContactDto_ShouldReturnFriendContact()
        {
            // Arrange
            var contactDto = new FriendContactDto { Name = "friend", TaxId = 1, Birthday = "birthday" };

            // Act
            var actual = contactMapper.MapToContact(contactDto) as FriendContact;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(FriendContact));
            Assert.AreEqual(contactDto.Name, actual.Name);
            Assert.AreEqual(contactDto.TaxId, actual.TaxId);
            Assert.AreEqual(contactDto.Birthday, actual.Birthday);
            Assert.AreEqual(contactDto.DateCreated, actual.DateCreated);
        }

        [TestMethod]
        public void TestContactMapper_MapToContact_WhenWorkContactDto_ShouldReturnWorkContact()
        {
            // Arrange
            var contactDto = new WorkContactDto { Name = "work", TaxId = 2, Title = "goober" };

            // Act
            var actual = contactMapper.MapToContact(contactDto) as WorkContact;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(WorkContact));
            Assert.AreEqual(contactDto.Name, actual.Name);
            Assert.AreEqual(contactDto.TaxId, actual.TaxId);
            Assert.AreEqual(contactDto.Title, actual.Title);
            Assert.AreEqual(contactDto.DateCreated, actual.DateCreated);
        }

        [TestMethod]
        public void TestContactMapper_MapToContact_WhenNotValidContactChildType_ShouldReturnNull()
        {
            // Arrange
            var contact = new Mock<ContactDto>();

            // Act
            var actual = contactMapper.MapToContact(contact.Object);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TestContactMapper_MapToContactDto_WhenNotValidContactChildType_ShouldReturnNull()
        {
            // Arrange
            var contact = new Mock<Contact>();

            // Act
            var actual = contactMapper.MapToContactDto(contact.Object);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TestContactMapper_MapToContactDtos_WhenAllContactChildTypes_ShouldAllContactChildDtoTypes()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>(ContactDataType.Company);
            var contact2 = ContactFactory.Get<FriendContact>(ContactDataType.Friend);
            var contact3 = ContactFactory.Get<WorkContact>(ContactDataType.Work);

            // Act
            var dtos = contactMapper.MapToContactDtos(new Contact[] { contact1, contact2, contact3} ).ToList();

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(dtos, typeof(ContactDto));
            CollectionAssert.AllItemsAreUnique(dtos);
            Assert.AreEqual(3, dtos.Count);
            Assert.IsInstanceOfType(dtos[0], typeof(CompanyContactDto));
            Assert.IsInstanceOfType(dtos[1], typeof(FriendContactDto));
            Assert.IsInstanceOfType(dtos[2], typeof(WorkContactDto));
        }
    }
}
