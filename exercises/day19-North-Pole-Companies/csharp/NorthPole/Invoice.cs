namespace NorthPole;

public class Invoice
{
    public string Customer { get; }
    public List<Delivery> Deliveries { get; }

    public Invoice(string customer, List<Delivery> deliveries)
    {
        Customer = customer;
        Deliveries = deliveries;
    }
}