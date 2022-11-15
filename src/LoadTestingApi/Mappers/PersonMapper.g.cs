using System.Collections.Generic;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;

namespace LoadTestingApi.MappingAbstractions
{
    public partial class PersonMapper : IPersonMapper
    {
        public PersonDto Map(PersonEntity p1)
        {
            return p1 == null ? null : new PersonDto()
            {
                ID = p1.ID,
                Name = p1.Name,
                Age = p1.Age,
                Email = p1.Email,
                Phones = funcMain1(p1.Phones)
            };
        }
        
        private List<PhoneDto> funcMain1(List<PhoneEntity> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<PhoneDto> result = new List<PhoneDto>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                PhoneEntity item = p2[i];
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