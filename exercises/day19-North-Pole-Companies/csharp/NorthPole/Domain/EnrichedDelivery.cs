namespace NorthPole.Domain;

public sealed class EnrichedDelivery(Delivery delivery, ElfCompany company, Tax tax)
{
    public int Packages { get; } = delivery.Packages;

    public Tax Tax => tax;
    public TaxRate TaxRate => tax.Rate;
    public string CompanyName => company.Name;
    public string CompanyType => company.Type;
}