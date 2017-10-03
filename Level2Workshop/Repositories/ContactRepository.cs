using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Level2Workshop.Model;
using Level2Workshop.Repositories.Def;
using Level2Workshop.Mapping;

namespace Level2Workshop.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        private List<ContactDto> _contacts;

        public ContactRepository(List<ContactDto> contacts = null)
        {
            Mapping.Config.SetupAutoMapper();
            this._contacts = contacts??new List<ContactDto>();
        }
        public bool Add(Contact item)
        {
            bool retval = false;
            if(item != null && !this.ContainsKey(item.TaxId))
            {
                this._contacts.Add(item.ToContactDto());
                retval = true;
            }
            return retval;
        }
        public bool Remove(Contact item)
        {
            bool retval = false;
            var contactDto = this._contacts.Where(w => w.TaxId == item.TaxId).FirstOrDefault();
            if (contactDto != null)
            {
                this._contacts.Remove(contactDto);
                retval = true;
            }
            return retval;
        }
        public Contact Get(int id)
        {
            Contact retval = null;
            var contactDto = this._contacts.Where(w => w.TaxId == id).FirstOrDefault();
            if (contactDto != null)
            {
              retval = contactDto.ToContact();
            }

            return retval;
        }

        public Contact Get(string name)
        {
            Contact retval = null;
            var contactDto = this._contacts.Where(w => w.Name == name).FirstOrDefault();
            if (contactDto != null)
            {
                retval = contactDto.ToContact();
            }

            return retval;
        }
        public IEnumerable<Contact> Get()
        {
            return this._contacts.ToContact();
        }
        public bool Update(Contact item,int id)
        {
            bool retval = false;
            var search = this._contacts.Where(w => w.TaxId == id).FirstOrDefault();
            if (search != null && search.ToContact().GetType() == item.GetType())
            {
                if (item.TaxId != id)
                {
                    if (this._contacts.Any(w => w.TaxId == item.TaxId))
                    {
                        throw new ArgumentException("contact tax ID must be unique within contact type");
                    }
                }
                search.TaxId = item.TaxId;
                search.Name = item.Name;
                search.PhoneNumber = item.PhoneNumber;
                search.Address = item.Address;
                search.DateModified = DateTime.Now;

                if(item is CompanyContact)
                {
                    var cContact = (CompanyContact)item;
                    var searchContact = (CompanyDto)search;

                    cContact.Url = searchContact.Url;

                }else if(item is WorkContact)
                {
                    var cContact = (WorkContact)item;
                    var searchContact = (WorkDto)search;

                    cContact.Title = searchContact.Title;
                    cContact.Url = searchContact.Url;
                    cContact.EmailAddress = searchContact.EmailAddress;
                }
                else
                {
                    var cContact = (FriendContact)item;
                    var searchContact = (FriendDto)search;

                    cContact.Birthday = searchContact.Birthday;
               }
                
            }
            
            return retval;
        }

        public bool ContainsKey(int itemId)
        {
           return this._contacts.Where(w => w.TaxId == itemId).Count() > 0;
        }
    }
}
