namespace NorthPole;

public record Invoice(string Customer, List<Delivery> Deliveries);