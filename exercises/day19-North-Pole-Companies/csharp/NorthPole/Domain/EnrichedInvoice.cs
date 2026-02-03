namespace NorthPole.Domain;

public class EnrichedInvoice(string customer, List<EnrichedDelivery> deliveries)
{
    public string Customer { get; } = customer;
    public List<EnrichedDelivery> Deliveries { get; } = deliveries;
}