using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Level2Workshop;
using Level2Workshop.Validations;
using Level2Workshop.Validations.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidationsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validations_TaxIdDefaultValue()
        {
            //Arrange
            var wc = new WorkContact() { Name = "Name"};

            //Act Assert
            Validations.validateRequired(wc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validations_NameNull()
        {
            //Arrange
            var wc = new WorkContact() { TaxId = 1 };

            //Act Assert
            Validations.validateRequired(wc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validations_TestNullableIntDefaultValue()
        {
            //Arrange
            var target = new TestNullableValueType();

            //Act Assert
            Validations.validateRequired(target);
        }

        internal class TestNullableValueType
        {
            [Required]
            public int? NotNull { get; set; }
        }
    }
}
