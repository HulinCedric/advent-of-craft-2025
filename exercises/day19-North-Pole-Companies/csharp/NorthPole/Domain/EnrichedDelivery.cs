namespace NorthPole.Domain;

public class EnrichedDelivery(Delivery delivery, EnrichedElfCompany company)
{
    public int Packages { get; } = delivery.Packages;
    public EnrichedElfCompany Company { get; } = company;
}