using LoadTestingApi.Shared.Enums;

namespace LoadTestingApi.Entities;

public sealed class PhoneEntity : IEquatable<PhoneEntity>
{
    public string Number { get; set; } = string.Empty;

    public PhoneType PhoneType { get; set; }

    public override string ToString() => $"number: {Number}, Phone Type: {PhoneType}";

    public override bool Equals(object? obj)
        => obj is PhoneEntity entity && Equals(entity);

    public static bool operator !=(PhoneEntity left, PhoneEntity right)
        => !(left == right);

    public static bool operator ==(PhoneEntity left, PhoneEntity right)
        => left.Equals(right);

    public bool Equals(PhoneEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number && PhoneType == other.PhoneType;
    }

    public override int GetHashCode()
        => (Number, PhoneType).GetHashCode();
}
