namespace NorthPole;

public class Delivery
{
    public string CompanyID { get; }
    public int Packages { get; }

    public Delivery(string companyID, int packages)
    {
        CompanyID = companyID;
        Packages = packages;
    }
}