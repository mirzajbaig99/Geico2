using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Level2Workshop.Repositories;
using Level2Workshop.Repositories.Def;
using Level2Workshop.Validations;

namespace Level2Workshop
{
    public class ContactsUi
    {
        private readonly IConsoleInterface consoleInterface;

        private readonly IRepository<Contact> _ContactRepository;

        //TODO: Refactor ContactsUi to use constructor injection for the ContactRepository
        //TODO: add a private readonly field for ContactRepository (typed as the interface it implements)
        public ContactsUi(IConsoleInterface consoleInterface = null, IRepository<Contact> repContact = null)
        {
            this.consoleInterface = consoleInterface ?? new ConsoleInterface();
            this._ContactRepository = repContact ?? new ContactRepository();
        }

        public void Run()
        {
            bool notFinished = true;

            do
            {
                try
                {
                    this.WriteCommandPrompt();
                    notFinished = this.ProcessLineCommand();
                }
                catch (Exception e)
                {
                    var errorMessage = string.Format("{0}: {1}", e.GetType().Name, e.Message);
                    consoleInterface.WriteLine(errorMessage);
                    //notFinished = false;
                }
            }
            while (notFinished);
        }

        public bool ProcessLineCommand()
        {
            var command = this.consoleInterface.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(command) || command.Trim().ToLower().Equals(ExerciseConstants.ExitCommand))
            {
                consoleInterface.WriteLine("Goodbye!");
                return false;
            }

            switch (command)
            {
                case ExerciseConstants.AddCommand:
                    this.AddContact();
                    break;

                case ExerciseConstants.EditCommand:
                    this.EditContact();
                    break;

                case ExerciseConstants.DeleteCommand:
                    this.DeleteContact();
                    break;

                case ExerciseConstants.SearchCommand:
                    this.SearchContacts();
                    break;

                case ExerciseConstants.PrintCommand:
                    this.PrintAll();
                    break;

                default:
                    this.DisplayHelp();
                    break;
            }

            return true;
        }

        private void AddContact()
        {
            // Removed Validations
            var contactType = this.GetPromptedData(ExerciseConstants.ContactTypePrompt).ToLower();

            var contact = CreateContactType(contactType);

            this._ContactRepository.Add(contact);
        }

        private Contact CreateContactType(string contactType)
        {
            switch (contactType)
            {
                case "friend":
                    return this.GetFriendContact();

                case "work":
                    return this.GetWorkData();

                case "company":
                    return this.GetCompanyData();

                default:
                    throw new ArgumentException("Contact type must be friend, work, or company");
            }
        }

        private FriendContact GetFriendContact(Contact contact = null, bool isAdd = true, int currTaxId = 0)
        {
            var friend = (FriendContact)contact ?? new FriendContact();

            GetCommonFields(friend, isAdd, currTaxId);

            friend.EmailAddress = this.GetPromptedData(ExerciseConstants.EmailPrompt);
            friend.Birthday = this.GetPromptedData(ExerciseConstants.BirthdayPrompt);

            return friend;
        }

        private WorkContact GetWorkData(Contact contact = null, bool isAdd = true, int currTaxId = 0)
        {
            var work = (WorkContact)contact ?? new WorkContact();

            GetCommonFields(work, isAdd, currTaxId);

            work.Title = this.GetPromptedData(ExerciseConstants.TitlePrompt);
            work.Company = this.GetPromptedData(ExerciseConstants.EmployerPrompt);
            work.EmailAddress = this.GetPromptedData(ExerciseConstants.EmailPrompt);
            work.Url = this.GetPromptedData(ExerciseConstants.UrlPrompt);

            return work;
        }

        private CompanyContact GetCompanyData(Contact contact = null, bool isAdd = true, int currTaxId = 0)
        {
            var company = (CompanyContact)contact ?? new CompanyContact();

            GetCommonFields(company, isAdd, currTaxId);

            company.Url = this.GetPromptedData(ExerciseConstants.UrlPrompt);

            return company;
        }

        private void GetCommonFields(Contact contact, bool addContact = false, int origTaxId = 0)
        {
            //TODO: remove required validation from the GetPromptedData method
            contact.Name = this.GetPromptedData(ExerciseConstants.NamePrompt);
        
            var newTaxId = Convert.ToInt32(this.GetPromptedData(ExerciseConstants.TaxIdPrompt));
            this.ValidateTaxId(newTaxId, origTaxId, addContact);
            contact.TaxId = newTaxId;

            contact.Address = this.GetPromptedData(ExerciseConstants.AddressPrompt);
            contact.PhoneNumber = this.GetPromptedData(ExerciseConstants.PhoneNumberPrompt);

            if (!addContact)
            {
                contact.DateModified = DateTime.Now;
            }
            //TODO: add call to generic Validation method for contact
            Validations.Validations.validateRequired<Contact>(contact);

        }

        private void ValidateTaxId(int newTaxId, int origTaxId, bool addContact)
        {
            if (newTaxId < 0 || newTaxId > 999999999)
            {
                throw new ArgumentOutOfRangeException("value", "Tax ID number must be between 1 and 999,999,999");
            }

            if ((addContact || !origTaxId.Equals(newTaxId)) && !this.TaxIdIsUnique(newTaxId))
            {
                throw new ArgumentException("New contact tax ID must be unique within contact type");
            }
        }

        //TODO: implement the check for unique Tax ID in the Repository (and call it from here)
        private bool TaxIdIsUnique(int taxId)
        {
            return !this._ContactRepository.ContainsKey(taxId);
        }

        private void EditContact()
        {
            //TODO: remove required validation from the GetPromptedData method
            var currentTaxId = Convert.ToInt32(this.GetPromptedData("current tax id: "));
            var contact = GetContact(Convert.ToInt32(currentTaxId));

            if (contact is FriendContact)
            {
                this._ContactRepository.Update(this.GetFriendContact(contact, false, currentTaxId), currentTaxId);
            }
            else if (contact is WorkContact)
            {
                this._ContactRepository.Update(this.GetWorkData(contact, false, currentTaxId), currentTaxId);
            }
            else if (contact is CompanyContact)
            {
                this._ContactRepository.Update(this.GetCompanyData(contact, false, currentTaxId), currentTaxId);
            }
        }

        private void DeleteContact()
        {
            //TODO: remove required validation from the GetPromptedData method
            string input = this.GetPromptedData(ExerciseConstants.TaxIdPrompt);

            int taxId = 0;
            if (!Int32.TryParse(input, out taxId))
            {
                throw new ArgumentException($"{ExerciseConstants.TaxIdPrompt} is required and should be a number");   
            }

            var contact = GetContact(Convert.ToInt32(taxId));
            this._ContactRepository.Remove(contact);
        }

        private void SearchContacts()
        {
            //TODO: remove required validation from the GetPromptedData method
            //var searchType = this.GetPromptedData(ExerciseConstants.SearchTypePrompt, true).ToLower();

            //if (!searchType.Equals("name") && !searchType.Equals("taxid"))
            //{
            //    throw new ArgumentException("Search type must be Name or TaxID");
            //}

            var prompt = ExerciseConstants.NamePrompt + " OR " +  ExerciseConstants.TaxIdPrompt;
            var input = this.GetPromptedData(prompt).Trim();

            if(string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"{prompt} is required");
            }

            int taxId = 0;
            var searchBytaxId = Int32.TryParse(input, out taxId);
            
            var contact = searchBytaxId
                ? this.GetContact(Convert.ToInt32(input))
                : this.GetContact(input);
            if (contact != null)
            {
                consoleInterface.WriteLine(contact.ToString());
            }
        }

        private Contact GetContact(string name)
        {
            var contact = this._ContactRepository.Get(name);
            if (contact == null)
            {
                throw new ArgumentException("No contact found with name of " + name);
            }

            return contact;
        }

        private Contact GetContact(int taxId)
        {
            var contact = this._ContactRepository.Get(taxId);
            if (contact == null)
            {
                throw new ArgumentException("No contact found with tax ID " + taxId);
            }

            return contact;
        }

        //TODO: remove "required" validation from the GetPromptedData method
        private string GetPromptedData(string prompt)//, bool inputRequired = false)
        {
            consoleInterface.WriteLine(prompt);
            
            var input = consoleInterface.ReadLine();

            //if (inputRequired && string.IsNullOrWhiteSpace(input))
            //{
            //    throw new ArgumentException("Required argument must be entered");
            //}

            return input;
        }

        private void PrintAll()
        {
            var addressBook = this._ContactRepository.Get();

            foreach (var c in addressBook)
            {
                consoleInterface.WriteLine(c);
            }

            if (addressBook.Count().Equals(0))
            {
                consoleInterface.WriteLine("Address book is empty");
            }
        }

        private void DisplayHelp()
        {
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

            foreach (string s in help)
            {
                consoleInterface.WriteLine(s);
            }

        }

        private void WriteCommandPrompt()
        {
            var prompt = string.Format(
                "\nEnter a command: {0}, {1}, {2}, {3}, {4}, or {5}",
                ExerciseConstants.AddCommand,
                ExerciseConstants.EditCommand,
                ExerciseConstants.DeleteCommand,
                ExerciseConstants.SearchCommand,
                ExerciseConstants.PrintCommand,
                ExerciseConstants.ExitCommand);

            consoleInterface.WriteLine(prompt);
        }
    }
}
