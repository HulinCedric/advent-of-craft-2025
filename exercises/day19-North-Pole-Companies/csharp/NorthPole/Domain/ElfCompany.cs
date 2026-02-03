namespace NorthPole.Domain;

public record ElfCompany(string Name, string Type, string RegionName)
{
    public const string ExpressType = "express";
    public const string StandardType = "standard";
}