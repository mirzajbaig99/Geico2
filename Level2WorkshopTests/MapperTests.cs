﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Level2Workshop;
using Level2Workshop.Repositories.Def;
using Level2Workshop.Model;
using Level2Workshop.Mapping;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapperTests
    {

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            Config.SetupAutoMapper();
        }

        [TestMethod]
        public void MapperTest_WorkContactToWorkDto()
        {
            //Arrange
            //Act
            WorkContact wc = new WorkContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                Url = "www.url.com",
                EmailAddress = "email@gmail.com",
                Title = "Title",
                PhoneNumber = "999-999-9999",
                Address = "Address"
            };

            WorkDto wDto = wc.ToWorkDto();
            //Assert
            Assert.AreEqual(wDto.Name,wc.Name);
            Assert.AreEqual(wDto.TaxId, wc.TaxId);
            Assert.AreEqual(wDto.DateCreated, wc.DateCreated);
            Assert.AreEqual(wDto.DateModified, wc.DateModified);
            Assert.AreEqual(wDto.EmailAddress, wc.EmailAddress);
            Assert.AreEqual(wDto.Url, wc.Url);
            Assert.AreEqual(wDto.Title, wc.Title);
            Assert.AreEqual(wDto.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(wDto.Address, wc.Address);
        }

        [TestMethod]
        public void MapperTest_WorkDtoToWorkContact()
        {
            //Arrange
            //Act
            WorkDto wc = new WorkDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                Url = "www.url.com",
                EmailAddress = "email@gmail.com",
                Title = "Title",
                PhoneNumber = "999-999-9999",
                Address = "Address"
            };

            WorkContact wDto = wc.ToWorkContact();
            //Assert
            Assert.AreEqual(wDto.Name, wc.Name);
            Assert.AreEqual(wDto.TaxId, wc.TaxId);
            Assert.AreEqual(wDto.DateCreated, wc.DateCreated);
            Assert.AreEqual(wDto.DateModified, wc.DateModified);
            Assert.AreEqual(wDto.EmailAddress, wc.EmailAddress);
            Assert.AreEqual(wDto.Url, wc.Url);
            Assert.AreEqual(wDto.Title, wc.Title);
            Assert.AreEqual(wDto.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(wDto.Address, wc.Address);
        }


        [TestMethod]
        public void MapperTest_FriendContactToFriendDto()
        {
            //Arrange
            //Act
            FriendContact fc = new FriendContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                EmailAddress = "email@gmail.com",
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Birthday = "01/01/1999",
                
            };

            FriendDto fDto = fc.ToFriendDto();
            //Assert
            Assert.AreEqual(fDto.Name, fc.Name);
            Assert.AreEqual(fDto.TaxId, fc.TaxId);
            Assert.AreEqual(fDto.DateCreated, fc.DateCreated);
            Assert.AreEqual(fDto.DateModified, fc.DateModified);
            Assert.AreEqual(fDto.EmailAddress, fc.EmailAddress);
            Assert.AreEqual(fDto.PhoneNumber, fc.PhoneNumber);
            Assert.AreEqual(fDto.Address, fc.Address);
            Assert.AreEqual(fDto.Birthday, fc.Birthday);
        }

        [TestMethod]
        public void MapperTest_FriendDtoToFriendContact()
        {
            //Arrange
            //Act
            FriendDto fDto = new FriendDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                EmailAddress = "email@gmail.com",
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Birthday = "01/01/1999"
            };

            FriendContact fc = fDto.ToFriendContact();
            //Assert
            Assert.AreEqual(fc.Name, fDto.Name);
            Assert.AreEqual(fc.TaxId, fDto.TaxId);
            Assert.AreEqual(fc.DateCreated, fDto.DateCreated);
            Assert.AreEqual(fc.DateModified, fDto.DateModified);
            Assert.AreEqual(fc.EmailAddress, fDto.EmailAddress);
            Assert.AreEqual(fc.Birthday, fDto.Birthday);
            Assert.AreEqual(fc.PhoneNumber, fDto.PhoneNumber);
            Assert.AreEqual(fc.Address, fDto.Address);
        }

        [TestMethod]
        public void MapperTest_CompanyContactToCompanyDto()
        {
            //Arrange
            //Act
            CompanyContact cc = new CompanyContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com"

            };

            CompanyDto cDto = cc.ToCompanyDto();
            //Assert
            Assert.AreEqual(cDto.Name, cc.Name);
            Assert.AreEqual(cDto.TaxId, cc.TaxId);
            Assert.AreEqual(cDto.DateCreated, cc.DateCreated);
            Assert.AreEqual(cDto.DateModified, cc.DateModified);
            Assert.AreEqual(cDto.Url, cc.Url);
            Assert.AreEqual(cDto.PhoneNumber, cc.PhoneNumber);
            Assert.AreEqual(cDto.Address, cc.Address);
        }

        [TestMethod]
        public void MapperTest_CompanyDtoToCompanyContact()
        {
            //Arrange
            //Act
            CompanyDto cDto = new CompanyDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com"
            };

            CompanyContact cc = cDto.ToCompanyContact();
            //Assert
            Assert.AreEqual(cc.Name, cDto.Name);
            Assert.AreEqual(cc.TaxId, cDto.TaxId);
            Assert.AreEqual(cc.DateCreated, cDto.DateCreated);
            Assert.AreEqual(cc.DateModified, cDto.DateModified);
            Assert.AreEqual(cc.Url, cDto.Url);
            Assert.AreEqual(cc.PhoneNumber, cDto.PhoneNumber);
            Assert.AreEqual(cc.Address, cDto.Address);
        }


        [TestMethod]
        public void MapperTest_ContactToContactDto_CompanyContact()
        {
            //Arrange
            //Act
            CompanyContact cc = new CompanyContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com"

            };

            ContactDto cDto = ((Contact)cc).ToContactDto();
            //Assert
            Assert.IsInstanceOfType(cDto, typeof(CompanyDto));
            Assert.AreEqual(cDto.Name, cc.Name);
            Assert.AreEqual(cDto.TaxId, cc.TaxId);
            Assert.AreEqual(cDto.DateCreated, cc.DateCreated);
            Assert.AreEqual(cDto.DateModified, cc.DateModified);
            Assert.AreEqual(((CompanyDto)cDto).Url, cc.Url);
            Assert.AreEqual(cDto.PhoneNumber, cc.PhoneNumber);
            Assert.AreEqual(cDto.Address, cc.Address);
        }

        [TestMethod]
        public void MapperTest_ContactDtoToContact_Company()
        {
            //Arrange
            //Act
            CompanyDto cc = new CompanyDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com"

            };

            Contact cDto = ((CompanyDto)cc).ToContact();
            //Assert
            Assert.IsInstanceOfType(cDto, typeof(CompanyContact));
            Assert.AreEqual(cDto.Name, cc.Name);
            Assert.AreEqual(cDto.TaxId, cc.TaxId);
            Assert.AreEqual(cDto.DateCreated, cc.DateCreated);
            Assert.AreEqual(cDto.DateModified, cc.DateModified);
            Assert.AreEqual(((CompanyContact)cDto).Url, cc.Url);
            Assert.AreEqual(cDto.PhoneNumber, cc.PhoneNumber);
            Assert.AreEqual(cDto.Address, cc.Address);
        }

        [TestMethod]
        public void MapperTest_ContactDtoToContact_Work()
        {
            //Arrange
            //Act
            WorkDto wDto = new WorkDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com",
                Title = "Title"
            };

            Contact cc = ((ContactDto)wDto).ToContact();
            //Assert
            Assert.AreEqual(cc.Name, wDto.Name);
            Assert.AreEqual(cc.TaxId, wDto.TaxId);
            Assert.AreEqual(cc.DateCreated, wDto.DateCreated);
            Assert.AreEqual(cc.DateModified, wDto.DateModified);
            Assert.AreEqual(((WorkContact)cc).Url, wDto.Url);
            Assert.AreEqual(((WorkContact)cc).Title, wDto.Title);
            Assert.AreEqual(cc.PhoneNumber, wDto.PhoneNumber);
            Assert.AreEqual(cc.Address, wDto.Address);
        }

        [TestMethod]
        public void MapperTest_ContactToContactDto_Work()
        {
            //Arrange
            //Act
            WorkContact wc = new WorkContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Url = "www.url.com",
                Title = "Title"
            };

            ContactDto cDto = ((Contact)wc).ToContactDto();
            //Assert
            Assert.AreEqual(cDto.Name, wc.Name);
            Assert.AreEqual(cDto.TaxId, wc.TaxId);
            Assert.AreEqual(cDto.DateCreated, wc.DateCreated);
            Assert.AreEqual(cDto.DateModified, wc.DateModified);
            Assert.AreEqual(((WorkDto)cDto).Url, wc.Url);
            Assert.AreEqual(((WorkDto)cDto).Title, wc.Title);
            Assert.AreEqual(cDto.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(cDto.Address, wc.Address);
        }

        [TestMethod]
        public void MapperTest_ContactDtoToContact_Friend()
        {
            //Arrange
            //Act
            FriendDto wDto = new FriendDto()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Birthday = "01/01/1999"
            };

            Contact cc = ((ContactDto)wDto).ToContact();
            //Assert
            Assert.AreEqual(cc.Name, wDto.Name);
            Assert.AreEqual(cc.TaxId, wDto.TaxId);
            Assert.AreEqual(cc.DateCreated, wDto.DateCreated);
            Assert.AreEqual(cc.DateModified, wDto.DateModified);
            Assert.AreEqual(((FriendContact)cc).Birthday, wDto.Birthday);
           Assert.AreEqual(cc.PhoneNumber, wDto.PhoneNumber);
            Assert.AreEqual(cc.Address, wDto.Address);
        }

        [TestMethod]
        public void MapperTest_ContactToContactDto_Friend()
        {
            //Arrange
            //Act
            FriendContact wc = new FriendContact()
            {
                TaxId = 1,
                Name = "Name",
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                PhoneNumber = "999-999-9999",
                Address = "Address",
                Birthday = "01/01/1999"
            };

            ContactDto cDto = ((Contact)wc).ToContactDto();
            //Assert
            Assert.AreEqual(cDto.Name, wc.Name);
            Assert.AreEqual(cDto.TaxId, wc.TaxId);
            Assert.AreEqual(cDto.DateCreated, wc.DateCreated);
            Assert.AreEqual(cDto.DateModified, wc.DateModified);
            Assert.AreEqual(((FriendDto)cDto).Birthday, wc.Birthday);
            Assert.AreEqual(cDto.PhoneNumber, wc.PhoneNumber);
            Assert.AreEqual(cDto.Address, wc.Address);
        }

       
    }
}
