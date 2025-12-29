using FluentAssertions;
using FluentAssertions.Primitives;

namespace AWorldWithoutMocksBefore.Tests.Verifications;

public static class ToyVerificationExtensions
{
    public static AndConstraint<EnumAssertions<ToyState>> InProduction(this Toy toy)
        => toy
            .State.Should()
            .Be(ToyState.InProduction);
}