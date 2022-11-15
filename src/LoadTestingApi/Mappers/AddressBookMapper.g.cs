using System.Collections.Generic;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;

namespace LoadTestingApi.MappingAbstractions
{
    public partial class AddressBookMapper : IAddressBookMapper
    {
        public AddressBookDto Map(AddressBookEntity p1)
        {
            return p1 == null ? null : new AddressBookDto() {Persons = funcMain1(p1.Persons)};
        }
        
        private List<PersonDto> funcMain1(List<PersonEntity> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<PersonDto> result = new List<PersonDto>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                PersonEntity item = p2[i];
                result.Add(funcMain2(item));
                i++;
            }
            return result;
            
        }
        
        private PersonDto funcMain2(PersonEntity p3)
        {
            return p3 == null ? null : new PersonDto()
            {
                ID = p3.ID,
                Name = p3.Name,
                Age = p3.Age,
                Email = p3.Email,
                Phones = funcMain3(p3.Phones)
            };
        }
        
        private List<PhoneDto> funcMain3(List<PhoneEntity> p4)
        {
            if (p4 == null)
            {
                return null;
            }
            List<PhoneDto> result = new List<PhoneDto>(p4.Count);
            
            int i = 0;
            int len = p4.Count;
            
            while (i < len)
            {
                PhoneEntity item = p4[i];
                result.Add(item == null ? null : new PhoneDto()
                {
                    Number = item.Number,
                    PhoneType = item.PhoneType
                });
                i++;
            }
            return result;
            
        }
    }
}