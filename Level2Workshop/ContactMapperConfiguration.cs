using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Level2Workshop
{
    public static class ContactMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<CompanyContact, CompanyContactDto>();
            Mapper.CreateMap<CompanyContactDto, CompanyContact>();
            Mapper.CreateMap<FriendContact, FriendContactDto>();
            Mapper.CreateMap<FriendContactDto, FriendContact>();
            Mapper.CreateMap<WorkContact, WorkContactDto>();
            Mapper.CreateMap<WorkContactDto, WorkContact>();
            //Mapper.CreateMap<Contact, ContactDto>().ReverseMap();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
