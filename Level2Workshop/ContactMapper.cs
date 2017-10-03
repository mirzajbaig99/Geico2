using System.Collections.Generic;
using AutoMapper;

namespace Level2Workshop
{
    public class ContactMapper
    {
        public Contact MapToContact(ContactDto contactDto)
        {
            switch (contactDto.GetType().Name)
            {
                case "CompanyContactDto":
                    return Mapper.Map<CompanyContact>(contactDto as CompanyContactDto);
                    
                case "FriendContactDto":
                    return Mapper.Map<FriendContact>(contactDto as FriendContactDto);
                    
                case "WorkContactDto":
                    return Mapper.Map<WorkContact>(contactDto as WorkContactDto);
            }
            return null;
        }

        public ContactDto MapToContactDto(Contact contact)
        {
            switch (contact.GetType().Name)
            {
                case "CompanyContact":
                    return this.MapToCompanyContactDto(contact as CompanyContact);
                    
                case "FriendContact":
                    return this.MapToFriendContactDto(contact as FriendContact);
                    
                case "WorkContact":
                    return this.MapToWorkContactDto(contact as WorkContact);
            }
            return null;
        }

        public IEnumerable<Contact> MapToContacts(IEnumerable<ContactDto> contactDtos)
        {
            foreach (var dto in contactDtos)
            {
                switch (dto.GetType().Name)
                {
                    case "CompanyContactDto":
                        yield return this.MapToCompanyContact(dto as CompanyContactDto);
                        break;

                    case "FriendContactDto":
                        yield return this.MapToFriendContact(dto as FriendContactDto);
                        break;

                    case "WorkContactDto":
                        yield return this.MapToWorkContact(dto as WorkContactDto);
                        break;
                }
            }
        }

        public IEnumerable<ContactDto> MapToContactDtos(IEnumerable<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                switch (contact.GetType().Name)
                {
                    case "CompanyContact":
                        yield return this.MapToCompanyContactDto(contact as CompanyContact);
                        break;

                    case "FriendContact":
                        yield return this.MapToFriendContactDto(contact as FriendContact);
                        break;

                    case "WorkContact":
                        yield return this.MapToWorkContactDto(contact as WorkContact);
                        break;
                }
            }
        }

        public CompanyContact MapToCompanyContact(CompanyContactDto companyContactDto)
        {
            return Mapper.Map<CompanyContact>(companyContactDto);
        }

        public CompanyContactDto MapToCompanyContactDto(CompanyContact companyContact)
        {
            return Mapper.Map<CompanyContactDto>(companyContact);
        }

        public FriendContact MapToFriendContact(FriendContactDto friendContactDto)
        {
            return Mapper.Map<FriendContact>(friendContactDto);
        }

        public FriendContactDto MapToFriendContactDto(FriendContact friendContact)
        {
            return Mapper.Map<FriendContactDto>(friendContact);
        }

        public WorkContact MapToWorkContact(WorkContactDto workContactDto)
        {
            return Mapper.Map<WorkContact>(workContactDto);
        }

        public WorkContactDto MapToWorkContactDto(WorkContact workContact)
        {
            return Mapper.Map<WorkContactDto>(workContact);
        }
    }
}
