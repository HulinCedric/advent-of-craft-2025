using Xunit;

namespace TimePortal.Tests;

public class TimePortalTest
{
    private readonly TimePortal _portal;
    private readonly Traveler _traveler;

    public TimePortalTest()
    {
        _portal = new TimePortal("PORTAL-001");
        _traveler = new Traveler("Dr. Elena Vance", "VISA-2024-001");
    }

    #region Activation

    [Fact(DisplayName = "Should be inactive by default")]
    public void ShouldBeInactiveByDefault()
    {
        Assert.False(_portal.IsActive);
    }

    [Fact(DisplayName = "Should be active after activation")]
    public void ShouldBeActiveAfterActivation()
    {
        _portal.Activate();
        Assert.True(_portal.IsActive);
    }

    [Fact(DisplayName = "Should be inactive after deactivation")]
    public void ShouldBeInactiveAfterDeactivation()
    {
        _portal.Activate();
        _portal.Deactivate();
        Assert.False(_portal.IsActive);
    }

    #endregion

    #region Charging

    [Fact(DisplayName = "Should start with zero energy")]
    public void ShouldStartWithZeroEnergy()
    {
        Assert.Equal(0, _portal.EnergyLevel);
    }

    [Fact(DisplayName = "Should increase energy when charged")]
    public void ShouldIncreaseEnergyWhenCharged()
    {
        _portal.Charge(50);
        Assert.Equal(50, _portal.EnergyLevel);
    }

    [Fact(DisplayName = "Should cap energy at maximum 500")]
    public void ShouldCapEnergyAtMaximum()
    {
        _portal.Charge(600);
        Assert.Equal(500, _portal.EnergyLevel);
    }

    [Fact(DisplayName = "Should ignore non-positive charge amounts")]
    public void ShouldIgnoreNonPositiveCharge()
    {
        _portal.Charge(100);
        _portal.Charge(-50);
        _portal.Charge(0);
        Assert.Equal(100, _portal.EnergyLevel);
    }

    #endregion

    #region Jump Initiation

    [Fact(DisplayName = "Should succeed with valid parameters")]
    public void ShouldSucceedWithValidParameters()
    {
        _portal.Activate();
        _portal.Charge(200);

        var result = _portal.InitiateJump(_traveler, 1985, "COORD001");

        Assert.StartsWith("SUCCESS", result);
    }

    [Fact(DisplayName = "Should consume energy on successful jump")]
    public void ShouldConsumeEnergyOnSuccess()
    {
        _portal.Activate();
        _portal.Charge(200);
        var energyBefore = _portal.EnergyLevel;

        _portal.InitiateJump(_traveler, 1985, "COORD001");

        Assert.True(_portal.EnergyLevel < energyBefore);
    }

    [Fact(DisplayName = "Should update traveler's current year")]
    public void ShouldUpdateTravelerYear()
    {
        _portal.Activate();
        _portal.Charge(200);

        _portal.InitiateJump(_traveler, 1985, "COORD001");

        Assert.Equal(1985, _traveler.CurrentYear);
    }

    [Fact(DisplayName = "Should fail when portal is inactive")]
    public void ShouldFailWhenInactive()
    {
        _portal.Charge(200);

        var result = _portal.InitiateJump(_traveler, 1985, "COORD001");

        Assert.StartsWith("ERROR", result);
    }

    [Fact(DisplayName = "Should fail when energy is insufficient")]
    public void ShouldFailWhenInsufficientEnergy()
    {
        var lowEnergyPortal = new TimePortal("LOW-ENERGY");
        lowEnergyPortal.Activate();
        lowEnergyPortal.Charge(50);

        var result = lowEnergyPortal.InitiateJump(_traveler, 1985, "COORD001");

        Assert.StartsWith("ERROR", result);
    }

    [Fact(DisplayName = "Should fail when destination year is out of range")]
    public void ShouldFailWhenYearOutOfRange()
    {
        _portal.Activate();
        _portal.Charge(200);

        var result = _portal.InitiateJump(_traveler, -15000, "COORD001");

        Assert.StartsWith("ERROR", result);
    }

    [Fact(DisplayName = "Should fail when coordinates are invalid")]
    public void ShouldFailWhenCoordinatesInvalid()
    {
        _portal.Activate();
        _portal.Charge(200);

        var nullCoords = _portal.InitiateJump(_traveler, 1985, null!);
        Assert.StartsWith("ERROR", nullCoords);

        var shortCoords = _portal.InitiateJump(_traveler, 1985, "SHORT");
        Assert.StartsWith("ERROR", shortCoords);
    }

    #endregion

    #region Travel History

    [Fact(DisplayName = "Should record successful jumps")]
    public void ShouldRecordJump()
    {
        _portal.Activate();
        _portal.Charge(200);
        _portal.InitiateJump(_traveler, 1985, "COORD001");

        var history = _portal.GetTravelHistory("Dr. Elena Vance");

        Assert.NotEmpty(history);
    }

    [Fact(DisplayName = "Should return empty for unknown traveler")]
    public void ShouldReturnEmptyForUnknownTraveler()
    {
        _portal.Activate();
        _portal.Charge(200);
        _portal.InitiateJump(_traveler, 1985, "COORD001");

        var history = _portal.GetTravelHistory("Unknown Person");

        Assert.Empty(history);
    }

    #endregion

    #region Portal Properties

    [Fact(DisplayName = "Should expose portal ID")]
    public void ShouldExposePortalId()
    {
        Assert.NotNull(_portal.PortalId);
    }

    [Fact(DisplayName = "Should expose status")]
    public void ShouldExposeStatus()
    {
        Assert.NotNull(_portal.Status);
    }

    #endregion
}
