namespace NorthPole.Domain;

public class EnrichedElfCompany(ElfCompany company, Tax tax)
{
    public string Name { get; } = company.Name;
    public string Type { get; } = company.Type;
    public Tax Tax { get; } = tax;
}