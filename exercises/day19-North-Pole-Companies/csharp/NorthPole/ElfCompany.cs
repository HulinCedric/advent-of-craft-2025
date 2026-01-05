namespace NorthPole;

public class ElfCompany
{
    public string Name { get; }
    public string Type { get; }
    public string Region { get; }

    public ElfCompany(string name, string type, string region)
    {
        Name = name;
        Type = type;
        Region = region;
    }

    public const string ExpressType = "express";
    public const string StandardType = "standard";
}