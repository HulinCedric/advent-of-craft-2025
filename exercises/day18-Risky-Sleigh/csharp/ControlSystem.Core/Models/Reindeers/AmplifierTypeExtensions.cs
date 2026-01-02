namespace ControlSystem.Core.Models.Reindeers;

public static class AmplifierTypeExtensions
{
    public static int GetMultiplier(this AmplifierType amplifierType) => (int)amplifierType;
}