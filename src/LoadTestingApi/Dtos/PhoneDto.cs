using LoadTestingApi.Shared.Enums;
using ProtoBuf;

namespace LoadTestingApi.Dtos;

[ProtoContract]
internal sealed class PhoneDto
{
    [ProtoMember(1)]
    public string Number { get; set; } = string.Empty;

    [ProtoMember(2)]
    public PhoneType PhoneType { get; set; }

    public override string ToString() => $"number: {Number}, Phone Type: {PhoneType}";
}
