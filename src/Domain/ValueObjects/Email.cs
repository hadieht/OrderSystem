using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    protected Email()
    {

    }
    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>("Email should not be empty");

        email = email.Trim();

        if (email.Length > 200)
            return Result.Failure<Email>("Email is too long");

        if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase))
            return Result.Failure<Email>("Email is invalid");

        return Result.Success(new Email(email));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }
}
