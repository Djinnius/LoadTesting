using LoadTestingApi.Shared.Enums;

namespace LoadTestingApi.Entities;

public sealed class PhoneEntity
{
    public string Number { get; set; } = string.Empty;

    public PhoneType PhoneType { get; set; }

    public override string ToString() => $"number: {Number}, Phone Type: {PhoneType}";
}
