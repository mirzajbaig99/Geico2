using System;
using System.Diagnostics.CodeAnalysis;
using Level2Workshop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidationTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidation_Validate_WhenRequiredPropertyIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            var contact = ContactFactory.Get<CompanyContact>();
            contact.Name = null;

            var validation = new Validation();

            // Act
            validation.Validate(contact);

            // Assert
        }
    }
}
