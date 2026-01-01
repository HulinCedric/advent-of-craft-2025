namespace ControlSystem.Core;

public static class AmplifierTypeExtensions
{
    public static int GetMultiplier(this AmplifierType amplifierType) => (int)amplifierType;
}