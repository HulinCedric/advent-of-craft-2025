using ArchUnitNET.Fluent.Conditions;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnitV3;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace TimePortal.Tests.Architecture;

public class PortalIsOpenWhen
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
        new ArchLoader()
            .LoadAssemblies(typeof(TimePortal).Assembly)
            .Build();

    public PortalIsOpenWhen()
    {
        Console.WriteLine("""
            
            ╔═══════════════════════════════════════════════════════════════╗
            ║  🌀 CHRONOS TEMPORAL GUARDIAN SYSTEM v3.14159                 ║
            ║  ───────────────────────────────────────────────────────────  ║
            ║  Scanning codebase for Object Calisthenics compliance...      ║
            ╚═══════════════════════════════════════════════════════════════╝
            """);
    }

    #region ⚡ Energy Containment Protocol

    [Fact(DisplayName = "🔋 Energy levels must be encapsulated in a value object")]
    public void EnergyLevelsShouldBeWrapped()
    {
        var rule = FieldMembers()
            .That().HaveName("_energyLevel")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field, 
                    field.Type.FullName != typeof(int).FullName,
                    "is a primitive int - create an Energy value object"),
                "Portal energy requires proper encapsulation to prevent temporal leaks");

        rule.Check(Architecture);
    }

    [Fact(DisplayName = "📅 Year fields must be wrapped in a Year value object")]
    public void YearsShouldBeWrapped()
    {
        var rule = FieldMembers()
            .That().HaveNameContaining("Year")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    field.Type.FullName != typeof(int).FullName,
                    "is a primitive int - create a Year value object"),
                "Temporal coordinates require Year value objects to enforce boundaries (-10000 to 3000)");

        rule.Check(Architecture);
    }

    #endregion

    #region 📜 Temporal Record Keeping

    [Fact(DisplayName = "📚 Travel logs must be encapsulated in a dedicated TravelLog type")]
    public void TravelLogsShouldBeFirstClassCollection()
    {
        var rule = FieldMembers()
            .That().HaveNameContaining("Log")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    !field.Type.FullName.Contains("System.Collections.Generic.List"),
                    "is a raw List - create a TravelLog first-class collection"),
                "Travel history requires a TravelLog collection class for proper temporal record keeping");

        rule.Check(Architecture);
    }

    [Fact(DisplayName = "🌐 Portal collections must be wrapped in a PortalRegistry")]
    public void PortalCollectionsShouldBeWrapped()
    {
        var rule = FieldMembers()
            .That().HaveName("_portals")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    !field.Type.FullName.Contains("System.Collections.Generic.List"),
                    "is a raw List - create a PortalRegistry first-class collection"),
                "Portal networks require a dedicated PortalRegistry for proper management");

        rule.Check(Architecture);
    }

    #endregion

    #region 🔒 Collection Integrity Shield

    [Fact(DisplayName = "🛡️ No property should expose raw List types")]
    public void ShouldNotExposeRawCollections()
    {
        var rule = PropertyMembers()
            .That().HaveNameContaining("Log").Or().HaveName("Portals")
            .Should().FollowCustomCondition(
                prop => new ConditionResult(prop,
                    !prop.Type.FullName.Contains("System.Collections.Generic.List"),
                    "exposes a raw List - return immutable views or domain-specific types"),
                "Exposing raw collections allows external temporal interference");

        rule.Check(Architecture);
    }

    #endregion

    #region 🎯 Status Type Safety

    [Fact(DisplayName = "⚡ Portal status must use an enum, not raw string")]
    public void PortalStatusShouldNotBeRawString()
    {
        var rule = FieldMembers()
            .That().HaveName("_currentStatus")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    field.Type.FullName != typeof(string).FullName,
                    "is a raw string - create a PortalStatus enum"),
                "Status indicators require type-safe enums (IDLE, CHARGING, JUMPING, ERROR)");

        rule.Check(Architecture);
    }

    [Fact(DisplayName = "❤️ Health status must use an enum, not raw string")]
    public void HealthStatusShouldBeEncapsulated()
    {
        var rule = FieldMembers()
            .That().HaveName("_healthStatus")
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    field.Type.FullName != typeof(string).FullName,
                    "is a raw string - create a HealthStatus enum"),
                "Traveler health requires type-safe status (STABLE, UNSTABLE, CRITICAL)");

        rule.Check(Architecture);
    }

    #endregion

    #region 🚀 FINAL ACTIVATION SEQUENCE

    [Fact(DisplayName = "🌀 All systems nominal - Portal ready for temporal operations")]
    public void AllSystemsNominal()
    {
        var rule = FieldMembers()
            .That().AreDeclaredIn(typeof(TimePortal))
            .Should().FollowCustomCondition(
                field => new ConditionResult(field,
                    field.Type.FullName != typeof(int).FullName,
                    "is a primitive int - all primitives must be wrapped"),
                "TimePortal must have no primitive int fields for stable temporal operations");

        rule.Check(Architecture);

        Console.WriteLine("""
            
            ╔═══════════════════════════════════════════════════════════════╗
            ║  🌀 CHRONOS: ALL SYSTEMS NOMINAL                             ║
            ║  ─────────────────────────────────────────────────────────── ║
            ║  ✅ Energy containment: STABLE                               ║
            ║  ✅ Temporal records: SECURED                                ║
            ║  ✅ Collection integrity: PROTECTED                          ║
            ║  ✅ Status encoding: TYPE-SAFE                               ║
            ║                                                               ║
            ║  🚀 PORTAL ACTIVATION: AUTHORIZED                            ║
            ╚═══════════════════════════════════════════════════════════════╝
            """);
    }

    #endregion
}
