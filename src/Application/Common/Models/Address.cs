namespace Application.Common.Models;

public record Address
{
    public string PostalCode { get; set; }

    public int HouseNumber { get; set; }

    public string AddressExtra { get; set; }
}