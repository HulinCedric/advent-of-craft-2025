using Xunit;

namespace TimePortal.Tests;

public class TravelerTest
{
    private readonly Traveler _traveler;

    public TravelerTest()
    {
        _traveler = new Traveler("Dr. Elena Vance", "VISA-2024-001");
    }

    #region Initial State

    [Fact(DisplayName = "Should have correct name")]
    public void ShouldHaveCorrectName()
    {
        Assert.Equal("Dr. Elena Vance", _traveler.Name);
    }

    [Fact(DisplayName = "Should have correct visa code")]
    public void ShouldHaveCorrectVisaCode()
    {
        Assert.Equal("VISA-2024-001", _traveler.VisaCode);
    }

    [Fact(DisplayName = "Should start with zero jumps")]
    public void ShouldStartWithZeroJumps()
    {
        Assert.Equal(0, _traveler.JumpCount);
    }

    [Fact(DisplayName = "Should start with STABLE health")]
    public void ShouldStartWithStableHealth()
    {
        Assert.Equal("STABLE", _traveler.GetHealthStatus());
    }

    [Fact(DisplayName = "Should not have temporal sickness initially")]
    public void ShouldNotHaveTemporalSicknessInitially()
    {
        Assert.False(_traveler.HasTemporalSickness());
    }

    #endregion

    #region Year Tracking

    [Fact(DisplayName = "Should track current year")]
    public void ShouldTrackCurrentYear()
    {
        _traveler.SetCurrentYear(1985);
        Assert.Equal(1985, _traveler.CurrentYear);
    }

    #endregion

    #region Jump Count and Health

    [Fact(DisplayName = "Should increment jump count")]
    public void ShouldIncrementJumpCount()
    {
        _traveler.IncrementJumpCount();
        Assert.Equal(1, _traveler.JumpCount);
    }

    [Fact(DisplayName = "Should degrade health after many jumps")]
    public void ShouldDegradeHealthAfterManyJumps()
    {
        for (var i = 0; i < 6; i++)
        {
            _traveler.IncrementJumpCount();
        }
        Assert.NotEqual("STABLE", _traveler.GetHealthStatus());
    }

    [Fact(DisplayName = "Should become critical after excessive jumps")]
    public void ShouldBecomeCriticalAfterExcessiveJumps()
    {
        for (var i = 0; i < 11; i++)
        {
            _traveler.IncrementJumpCount();
        }
        Assert.Equal("CRITICAL", _traveler.GetHealthStatus());
    }

    #endregion

    #region Temporal Sickness

    [Fact(DisplayName = "Should have temporal sickness after excessive jumps")]
    public void ShouldHaveTemporalSicknessAfterManyJumps()
    {
        for (var i = 0; i < 11; i++)
        {
            _traveler.IncrementJumpCount();
        }
        Assert.True(_traveler.HasTemporalSickness());
    }

    [Fact(DisplayName = "Should have temporal sickness immediately after a jump")]
    public void ShouldHaveTemporalSicknessRightAfterJump()
    {
        _traveler.IncrementJumpCount();
        Assert.True(_traveler.HasTemporalSickness());
    }

    #endregion
}
