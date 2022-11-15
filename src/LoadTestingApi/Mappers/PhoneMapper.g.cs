using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;

namespace LoadTestingApi.MappingAbstractions
{
    public partial class PhoneMapper : IPhoneMapper
    {
        public PhoneDto Map(PhoneEntity p1)
        {
            return p1 == null ? null : new PhoneDto()
            {
                Number = p1.Number,
                PhoneType = p1.PhoneType
            };
        }
    }
}