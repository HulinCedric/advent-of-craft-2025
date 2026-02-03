namespace NorthPole.Domain;

public sealed class EnrichedDelivery(Delivery delivery, EnrichedElfCompany company)
{
    public int Packages { get; } = delivery.Packages;

    public Tax Tax => company.Tax;
    public string CompanyName => company.Name;
    public string CompanyType => company.Type;
}