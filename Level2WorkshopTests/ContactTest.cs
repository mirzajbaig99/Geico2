using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Level2Workshop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContactTest
    {
        [TestMethod]
        public void TestContact_Equals_WhenParmNotContact_ShouldReturnFalse()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            var obj = new object();

            // Act
            var condition = contact.Equals(obj);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TestContact_Equals_WhenTaxIdNotEqual_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>();
            var contact2 = ContactFactory.Get<CompanyContact>();
            contact2.TaxId++;

            // Act
            var condition = contact1.Equals(contact2);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TestContact_Equals_WhenDateCreatedNotEqual_ShouldReturnFalse()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>();
            Thread.Sleep(500);
            var contact2 = ContactFactory.Get<CompanyContact>();

            // Act
            var condition = contact1.Equals(contact2);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TestContact_Equals_WhenAllPropertiesEqual_ShouldReturnTrue()
        {
            // Arrange
            var contact1 = ContactFactory.Get<CompanyContact>();
            var contact2 = ContactFactory.Get<CompanyContact>();

            // Act
            var condition = contact1.Equals(contact2);

            // Assert
            Assert.IsTrue(condition);
        }
    }
}
