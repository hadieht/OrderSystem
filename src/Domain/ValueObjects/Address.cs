using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class Address : ValueObject
{
    public string PostalCode { get; }

    public int HouseNumber { get; }

    public string Extra { get; }

    public static Result<Address> Create(string postalCode, int houseNumber, string extra)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return Result.Failure<Address>("PostalCode should not be empty");

        if (houseNumber <= 0)
            return Result.Failure<Address>("The house number cannot be less than zero");

        return Result.Success(new Address(postalCode, houseNumber, extra));
    }

    private Address(string postalCode, int houseNumber, string extra)
    {
        this.PostalCode = postalCode;
        this.HouseNumber = houseNumber;
        this.Extra = extra;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PostalCode;
        yield return HouseNumber;
        yield return Extra;
    }
}
