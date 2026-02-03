namespace NorthPole.Domain;

public record Invoice(string Customer, List<Delivery> Deliveries);