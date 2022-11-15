using System.Text;

namespace LoadTestingApi.Entities;

public sealed class PersonEntity : IEquatable<PersonEntity>
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    public List<PhoneEntity> Phones { get; set; } = new List<PhoneEntity>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"ID: {ID}");
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Age: {Age}");
        sb.AppendLine($"Email: {Email}");

        foreach (var phone in Phones)
            sb.AppendLine($"Phone: {phone}");

        return sb.ToString();
    }

    public override bool Equals(object? obj)
        => obj is PersonEntity entity && Equals(entity);

    public static bool operator !=(PersonEntity left, PersonEntity right)
        => !(left == right);

    public static bool operator ==(PersonEntity left, PersonEntity right)
        => left.Equals(right);

    public bool Equals(PersonEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return ID == other.ID &&
            Name == other.Name &&
            Age == other.Age &&
            Email == other.Email &&
            Phones.SequenceEqual(other.Phones);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = (ID, Name, Age, Email).GetHashCode();

            foreach (var phone in Phones)
                hash = hash * 31 + phone.GetHashCode();

            return hash;
        }
    }
}
