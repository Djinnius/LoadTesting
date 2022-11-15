using System.Text;

namespace LoadTestingApi.Entities;

public sealed class AddressBookEntity : IEquatable<AddressBookEntity>
{
    public List<PersonEntity> Persons { get; set; } = new List<PersonEntity>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var person in Persons)
            sb.AppendLine($"Person {person}");

        return sb.ToString();
    }

    public override bool Equals(object? obj)
        => obj is AddressBookEntity entity && Equals(entity);

    public static bool operator !=(AddressBookEntity left, AddressBookEntity right)
        => !(left == right);

    public static bool operator ==(AddressBookEntity left, AddressBookEntity right)
        => left.Equals(right);

    public bool Equals(AddressBookEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Persons.SequenceEqual(other.Persons);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 19;

            foreach (var person in Persons)
                hash = hash * 31 + person.GetHashCode();

            return hash;
        }
    }
}
