using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Level2Workshop.Model;

namespace Level2Workshop.Mapping
{
    public static class Extensions
    {

        public static WorkDto ToWorkDto(this WorkContact item)
        {
            return Mapper.Map<WorkDto>(item);
        }

        public static FriendDto ToFriendDto(this FriendContact item)
        {
            return Mapper.Map<FriendDto>(item);
        }

        public static CompanyDto ToCompanyDto(this CompanyContact item)
        {
            return Mapper.Map<CompanyDto>(item);
        }

        public static WorkContact ToWorkContact(this WorkDto item)
        {
            return Mapper.Map<WorkContact>(item);
        }


        public static FriendContact ToFriendContact(this FriendDto item)
        {
            return Mapper.Map<FriendContact>(item);
        }

        public static CompanyContact ToCompanyContact(this CompanyDto item)
        {
            return Mapper.Map<CompanyContact>(item);
        }

        public static ContactDto ToContactDto(this Contact item)
        {
            return Mapper.Map<ContactDto>(item);
        }

        public static Contact ToContact(this ContactDto item)
        {
            return Mapper.Map<Contact>(item);
        }

        public static IEnumerable<Contact> ToContact(this IEnumerable<ContactDto> item)
        {
            return Mapper.Map<IEnumerable<Contact>>(item);
        }
    }
}
