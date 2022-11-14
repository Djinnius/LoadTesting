using LoadTestingApi.Shared.Enums;
using ProtoBuf;

namespace LoadTestingApi.Entities;

internal sealed class PhoneEntity
{
    public string Number { get; set; } = string.Empty;

    public PhoneType PhoneType { get; set; }

    public override string ToString() => $"number: {Number}, Phone Type: {PhoneType}";
}
