using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Level2Workshop;
using Level2Workshop.Repositories.Def;

namespace Level2WorkshopTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContactsUiTest
    {

        private static MockRepository mockRepository;
        private static Mock<IConsoleInterface> mockConsole;
        private StringBuilder outputBuilder = new StringBuilder();
        private Queue<string> consoleInputParams = null;
        

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
        }

        [TestInitialize]
        public void TestSetup()
        {
            consoleInputParams = new Queue<string>();
            outputBuilder = new StringBuilder();

            mockConsole = mockRepository.Create<IConsoleInterface>();
            Func<string> consoledequeue = () =>
            {
                if (this.consoleInputParams.Count == 0)
                {
                    return string.Empty;
                }
                return consoleInputParams.Dequeue();
            };
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>()))
                .Callback((object x) =>
                {
                    if (outputBuilder != null) { outputBuilder.Append(x.ToString() + "\r\n"); }
                });
            mockConsole.Setup(x => x.Write(It.IsAny<object>()))
            .Callback((object x) =>
            {
                if (outputBuilder != null) { outputBuilder.Append(x.ToString()); }
            });

        }

        [TestMethod]
        public void ContactUiTest_ConstructorWithoutConsoleInterface()
        {
            //Arrange
            //Act
            var contactsUi = new ContactsUi();

            //Assert
            Assert.IsNotNull(contactsUi);
            Assert.IsInstanceOfType(contactsUi, typeof(ContactsUi));
        }

        [TestMethod]
        public void ContactUiTest_ExitWritesGoodbye()
        {
            //Arrange
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new [] { "exit" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.Run();

            //Assert
            var output = outputBuilder.ToString();
            StringAssert.Contains(output, "Goodbye!\r\n");
        }

        [TestMethod]
        public void ContactUiTest_BlankReturnsFalse()
        {
            //Arrange
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>()));

            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(false, returnValue);
        }

        [TestMethod]
        public void ContactUiTest_ExitReturnsFalse()
        {
            //Arrange
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "exit" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())); 

            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(false, returnValue);
        }

        [TestMethod]
        public void ContactUiTest_AddFriendSuccessfully()
        {
            //Arrange
            var inputParms = new List<string>();
            inputParms.AddRange(new[] { "add", "friend" });
            inputParms.AddRange(FakeData.GetFriendContactData());

            List<Contact> contacts = new List<Contact>();
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(inputParms.ToArray()).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });
            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
                x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual(typeof(FriendContact).Name, contacts[0].GetType().Name);
            Assert.AreEqual("testname3", contacts[0].Name);
            Assert.AreEqual("1/2/90", ((FriendContact)contacts[0]).Birthday);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_Add_TaxIdMustbeUnique_Fail()
        {
            //Arrange
            var inputParms = new List<string>();
            inputParms.AddRange(new[] { "add", "friend" });
            inputParms.AddRange(FakeData.GetFriendContactData());

            List<Contact> contacts = new List<Contact>()
            {
                FakeDataFactory.Get<FriendContact>(taxId:3)
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(inputParms.ToArray()).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });
            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
                x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act Assert
            var returnValue = contactsUi.ProcessLineCommand();
           
        }

        [TestMethod]
        public void ContactUiTest_AddWorkSuccessful()
        {
            //Arrange
            var inputParms = new List<string>();
            inputParms.AddRange(new[] { "add", "work" });
            inputParms.AddRange(FakeData.GetWorkContactData());
            
            List<Contact> contacts = new List<Contact>();
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(inputParms.ToArray()).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });
            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
                x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual(typeof(WorkContact).Name, contacts[0].GetType().Name);
            Assert.AreEqual("www.mycompany.com", ((WorkContact)contacts[0]).Url);
        }

        [TestMethod]
        public void ContactUiTest_AddCompanyNoUrlSuccessful()
        {
            //Arrange
            var inputParms = new List<string>();
            inputParms.AddRange(new[] { "add", "company" });
            inputParms.AddRange(FakeData.GetCompanyContactData());

            List<Contact> contacts = new List<Contact>();
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });
            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
                x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(inputParms.ToArray()).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual(typeof(CompanyContact).Name, contacts[0].GetType().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_AddDuplicateTaxIdThrowsArgumentException()
        {
            //Arrange
            List<Contact> contacts = new List<Contact>();
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
                x => (contacts.Where(w => w.TaxId == x).Count() > 0));
           

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            var inputParms = new Queue<string>(new[] { "add", "friend", "name", "1" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });
            
            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);
            contactsUi.ProcessLineCommand();
            inputParms = new Queue<string>(new[] { "add", "friend", "name", "1" });
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);

            contactsUi = new ContactsUi(mockConsole.Object,rep.Object);
            
            //Act Assert
            contactsUi.ProcessLineCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ContactUiTest_AddNegativeTaxIdThrowsArgumentOutOfRangeException()
        {
            //Arrange
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "add", "friend", "name", "-1" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

             var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ContactUiTest_AddLargeTaxIdThrowsArgumentOutOfRangeException()
        {
            //Arrange
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "add", "friend", "name", "1000000000" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

           var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_AddWithNoTypeThrowsArgumentException()
        {
            //Arrange
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "add","" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_AddWithBadTaxIdThrowsException()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "add", "friend", "name", "x" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });
            
            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.Run();

            //Assert
            StringAssert.Contains(outputBuilder.ToString(), "FormatException: Input string was not in a correct format.\r\n");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_AddWithBadTypeThrowsArgumentException()
        {
            //Arrange
            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(new Queue<string>(new[] { "add", "crazy", "type" }).Dequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_EditFriendNewTaxIdSuccessful()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "edit", "1", "new name", "2" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contact = FakeDataFactory.Get<FriendContact>(name:"name",taxId:1);
            
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());
               
            rep.Setup(r => r.Add(It.IsAny<Contact>())).Returns(true)
                .Callback((Contact x) => { contacts.Add(x); });

            rep.Setup(r => r.Update(It.IsAny<Contact>(), It.IsAny<int>())).Returns(true)
                .Callback<Contact, int>((x, y) => { contacts.Remove(contact); contacts.Add(x); });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
               x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual("new name", contacts[0].Name);
            Assert.AreEqual(2, contacts[0].TaxId);
        }

        [TestMethod]
        public void ContactUiTest_EditCompanySuccessful()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "edit", "1", "new name", "1", "", "", "www.newurl.com" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contact = FakeDataFactory.Get<CompanyContact>(name: "name", taxId: 1);

            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            rep.Setup(r => r.Update(It.IsAny<Contact>(), It.IsAny<int>())).Returns(true)
                .Callback<Contact, int>((x, y) => { contacts.Remove(contact); contacts.Add(x); });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
               x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);
            

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual("new name", contacts[0].Name);
            Assert.AreEqual("www.newurl.com", ((CompanyContact)contacts[0]).Url);
        }

        [TestMethod]
        public void ContactUiTest_EditWorkSuccessful()
        {
            //Arrange
            var inputParms = new Queue<string>(new[]  {
                        "edit", "1", "new name", "1", "", "", "new title", "new co", "NewEmail@noreply.com",
                        "www.newurl.com"
                    });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contact = FakeDataFactory.Get<WorkContact>( name:"name", taxId:1);
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
                .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            rep.Setup(r => r.Update(It.IsAny<Contact>(), It.IsAny<int>())).Returns(true)
                .Callback<Contact, int>((x, y) => { contacts.Remove(contact); contacts.Add(x); });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
               x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual("new name", contacts[0].Name);
            Assert.AreEqual("new title", ((WorkContact)contacts[0]).Title);
            Assert.AreEqual("new co", ((WorkContact)contacts[0]).Company);
            Assert.AreEqual("NewEmail@noreply.com", ((WorkContact)contacts[0]).EmailAddress);
            Assert.AreEqual("www.newurl.com", ((WorkContact)contacts[0]).Url);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_EditNotFoundThrowsArgumentException()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "edit", "2", "newName", "2" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contact = FakeDataFactory.Get<FriendContact>( name : "name", taxId : 1 );

            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            rep.Setup(r => r.Update(It.IsAny<Contact>(), It.IsAny<int>())).Returns(true)
                .Callback<Contact, int>((x, y) => { contacts.Remove(contact); contacts.Add(x); });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
               x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);
            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_EditToDuplicateTaxIdThrowsArgumentException()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "edit", "1", "newName", "2" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

            var contact1 = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1 );
            var contact2 = FakeDataFactory.Get<FriendContact>(name:"name", taxId:2 );
            List<Contact> contacts = new List<Contact>() { contact1,contact2 };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            rep.Setup(r => r.Update(It.IsAny<Contact>(), It.IsAny<int>())).Returns(true)
                .Callback<Contact,int>((x,y) => {
                    if(y != x.TaxId && contacts.Any(w => w.TaxId == y))
                        throw new ArgumentException();
                        });

            rep.Setup(r => r.ContainsKey(It.IsAny<int>())).Returns<int>(
               x => (contacts.Where(w => w.TaxId == x).Count() > 0));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);
            
            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_EditWithNoArgsThrowsArgumentException()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "edit", "1" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });

             var contactsUi = new ContactsUi(mockConsole.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_DeleteSuccessful()
        {
            //Arrange
            var inputParms = new Queue<string>(new[] { "delete", "1" });
            Func<string> consoledequeue = () =>
            {
                if (inputParms.Count == 0)
                {
                    return string.Empty;
                }
                return inputParms.Dequeue();
            };

            StringBuilder outputBuilder = new StringBuilder(string.Empty);
            mockConsole.Setup(x => x.ReadLine()).Returns(consoledequeue);
            mockConsole.Setup(x => x.WriteLine(It.IsAny<object>())).Callback((object x) => { outputBuilder.Append(x.ToString() + "\r\n"); });
            var contact = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1);
            
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());
            rep.Setup(r => r.Remove(It.IsAny<Contact>())).Returns(true)
             .Callback((Contact x) => contacts.Remove(x));

            var contactsUi = new ContactsUi(mockConsole.Object, rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_DeleteNotFoundThrowsArgumentException()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1);

            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());
            rep.Setup(r => r.Remove(It.IsAny<Contact>())).Returns(true)
             .Callback((Contact x) => contacts.Remove(x));

            var contactsUi = new ContactsUi(setupConsole(new[] { "delete", "2" }), rep.Object);
           
            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_DeleteWithNoArgsThrowsArgumentException()
        {
            //Arrange
            var contactsUi = new ContactsUi(setupConsole(new[] { "delete" }));

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_SearchByNameSuccessful()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name",taxId:1);

            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<string>()))
             .Returns((string x) => contacts.Where(w => w.Name == x).FirstOrDefault());
           
            var contactsUi = new ContactsUi(setupConsole(new[] { "search", "name" }), rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.IsTrue(outputBuilder.ToString().Contains("Friend"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_SearchByNameNotFoundThrowsArgumentException()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1);
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();
            var contactsUi = new ContactsUi(setupConsole(new[] { "search", "wrong" }), rep.Object);

            rep.Setup(r => r.Get(It.IsAny<string>()))
            .Returns((string x) => contacts.Where(w => w.Name == x).FirstOrDefault());

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_SearchByTaxIdSuccessful()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1 );
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            var contactsUi = new ContactsUi(setupConsole(new[] { "search", "1" }), rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.IsTrue(outputBuilder.ToString().Contains("Friend"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_SearchByTaxIdThrowsArgumentException()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name",taxId:2);
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<int>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            var contactsUi = new ContactsUi(setupConsole(new[] { "search", "1" }), rep.Object);

            //Act
            var returnValue = contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(true, returnValue);
            Assert.IsTrue(outputBuilder.ToString().Contains("Friend"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_SearchByBadTypeThrowsArgumentException()
        {
            //Arrange
            var contact = FakeDataFactory.Get<FriendContact>(name:"name", taxId:1 );
            List<Contact> contacts = new List<Contact>() { contact };
            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();

            rep.Setup(r => r.Get(It.IsAny<string>()))
             .Returns((int x) => contacts.Where(w => w.TaxId == x).FirstOrDefault());

            var contactsUi = new ContactsUi(setupConsole(new[] { "search", "hahaha", "2" }), rep.Object);

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContactUiTest_SearchWithNoArgsThrowsArgumentException()
        {
            //Arrange
            var contactsUi = new ContactsUi(setupConsole(new[] { "search" }));

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
        }

        [TestMethod]
        public void ContactUiTest_PrintSuccessful()
        {
            //Arrange
            Contact contact1 = FakeDataFactory.Get<FriendContact>(1,"name",null,null,null,null,null,null,null,null );
            Contact contact2 = FakeDataFactory.Get<WorkContact>(2, "name", null, null, null, null, null, null, null, null);
            Contact contact3 = FakeDataFactory.Get<CompanyContact>(3, "name", null, null, null, null, null, null, null, null);

            Mock<IRepository<Contact>> rep = ContactsUiTest.mockRepository.Create<IRepository<Contact>>();
            List<Contact> contacts = new List<Contact>() { contact1, contact2, contact3 };
            rep.Setup(r => r.Get())
             .Returns(contacts);

            var contactsUi = new ContactsUi(setupConsole(new[] { "print" }), rep.Object);

            //Act
            contactsUi.ProcessLineCommand();
            string output = outputBuilder.ToString();
            //Assert
            Assert.IsTrue(output.Contains(
                "Friend\r\n-------\r\nName: name\r\nTax ID: 1\r\nAddress: \r\nPhone Number: \r\nEmail Address: \r\nBirthday: \r\nDate Created:"
                ));
            Assert.IsTrue(output.Contains(
                "Work\r\n-------\r\nName: name\r\nTax ID: 2\r\nAddress: \r\nPhone Number: \r\nTitle: \r\nCompany: \r\nEmail Address: \r\nURL: \r\nDate Created:"
                ));
            Assert.IsTrue(output.Contains(
                "Company\r\n-------\r\nName: name\r\nTax ID: 3\r\nAddress: \r\nPhone Number: \r\nURL: \r\nDate Created: "
                ));
        }

        [TestMethod]
        public void ContactUiTest_PrintEmptyAddressBookWritesMessage()
        {
            //Arrange
            var contactsUi = new ContactsUi(setupConsole(new[] { "print" }));

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual("Address book is empty\r\n", outputBuilder.ToString());
        }

        [TestMethod]
        public void ContactUiTest_HelpDisplayedForHelpCommand()
        {
            //Arrange
           var contactsUi = new ContactsUi(setupConsole(new[] { "help" }));
            var expectedOutput = this.CreateExpectedHelpOutput();

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(expectedOutput, outputBuilder.ToString());
        }

        [TestMethod]
        public void ContactUiTest_HelpDisplayedForBadCommand()
        {
            //Arrange
            var contactsUi = new ContactsUi(setupConsole(new[] { "wootwootwoot" }));
            var expectedOutput = this.CreateExpectedHelpOutput();

            //Act
            contactsUi.ProcessLineCommand();

            //Assert
            Assert.AreEqual(expectedOutput, outputBuilder.ToString());
        }

        private string CreateExpectedHelpOutput()
        {
            var expectedOutput = new StringBuilder();

            var help = new[]
                {
                    string.Empty,
                    "Valid commands:",
                    string.Empty,
                    "  add friend <name*> <tax ID*> ... (fields as prompted)",
                    "  add work <name*> <tax ID*> ... (fields as prompted)",
                    "  add company <name*> <tax ID*> ... (fields as prompted)",
                    "  search Name <name*>",
                    "  search TaxID <tax ID*>",
                    "  edit <current tax ID*> <name*> <tax ID*> ... (fields as prompted)",
                    "  delete <tax ID*>",
                    "  print",
                    string.Empty,
                    "NOTE: * = required, [] = optional",
                    "All fields except address must be a single word (no spaces)",
                    string.Empty
                };

            foreach (string h in help)
            {
                expectedOutput.Append(h);
                expectedOutput.Append("\r\n");
            }

            return expectedOutput.ToString();
           }


        private IConsoleInterface setupConsole
            (IEnumerable<string> inputprams)
        {
            this.consoleInputParams = new Queue<string>(inputprams);
            return mockConsole.Object;
        }
    }

        
}
