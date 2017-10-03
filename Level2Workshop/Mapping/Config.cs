using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Level2Workshop.Model;

namespace Level2Workshop.Mapping
{
    
    public static class Config
    {
        public static void SetupAutoMapper()
        {
            Mapper.CreateMap<CompanyDto, CompanyContact>().ReverseMap();
            Mapper.CreateMap<WorkDto, WorkContact>().ReverseMap();
            Mapper.CreateMap<FriendDto, FriendContact>().ReverseMap();

            Mapper.CreateMap<Contact, ContactDto>().ConstructUsing((Contact src) =>
             {
                 Type t = src.GetType();
                 if (t == typeof(WorkContact))
                 {
                     return Mapper.Map<WorkDto>(src);
                 }
                 else if (t == typeof(CompanyContact))
                 {
                     return Mapper.Map<CompanyDto>(src);
                 }
                 else 
                 {
                     return Mapper.Map<FriendDto>(src);
                 }
               
             });

            Mapper.CreateMap<ContactDto, Contact> ().ConstructUsing((ContactDto src) =>
            {
                Type t = src.GetType();
                if (t == typeof(WorkDto))
                {
                    return Mapper.Map<WorkContact>(src);
                }
                else if (t == typeof(CompanyDto))
                {
                    return Mapper.Map<CompanyContact>(src);
                }
                else 
                {
                    return Mapper.Map<FriendContact>(src);
                }
                
            });
            Mapper.AssertConfigurationIsValid();
        }

    }
    
}
