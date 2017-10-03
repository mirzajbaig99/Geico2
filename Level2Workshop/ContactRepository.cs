using System;
using System.Collections.Generic;
using System.Linq;

namespace Level2Workshop
{
    public class ContactRepository : IRepository<Contact>
    {
        #region Private fields

        private readonly List<ContactDto> contactDtos;

        private readonly ContactMapper contactMapper;

        #endregion

        public ContactRepository(List<ContactDto> dtos = null)
        {
            contactDtos = dtos ?? new List<ContactDto>();
            contactMapper = new ContactMapper();
        }

        public bool Add(Contact item)
        {
            if (item == null)
            {
                return false;
            }

            if (contactDtos.Any(dto => dto.TaxId == item.TaxId))
            {
                throw new ArgumentException("Cannot add contact with duplicate Tax ID");
            }

            // TODO: add name/tax id populated validation
            var validation = new Validation();
            validation.Validate(item);

            var contactDto = contactMapper.MapToContactDto(item);
            contactDtos.Add(contactDto);
            return true;
        }

        public bool Add(IEnumerable<Contact> items)
        {
            return items.Aggregate(true, (current, item) => current & Add(item));
        }

        public IEnumerable<Contact> Get()
        {
            var contacts = contactMapper.MapToContacts(contactDtos);
            return contacts;
        }

        public Contact Get(int id)
        {
            var dto = contactDtos.FirstOrDefault(c => c.TaxId == id);
            return (dto == null) 
                ? null 
                : contactMapper.MapToContact(dto);
        }

        public Contact Get(string name)
        {
            var dto = contactDtos.FirstOrDefault(c => c.Name == name);
            return (dto == null) 
                ? null 
                : contactMapper.MapToContact(dto);
        }

        //public int GetNextId()
        //{
        //    return (contactDtos.Count == 0)
        //        ? 1
        //        : contactDtos.Max(c => c.TaxId) + 1;
        //}

        public bool Update(Contact item, int currentId)
        {
            if (item != null && this.Remove(new Contact { TaxId = currentId }))
            {
                Add(item);
                return true;
            }
            return false;
        }

        public bool Remove(Contact item)
        {
            var contactDto = item == null
                ? null
                : contactDtos.FirstOrDefault(c => c.TaxId == item.TaxId);

            if (contactDto != null)
            {
                contactDtos.Remove(contactDto);
            }
            return contactDto != null;
        }
    }
}
