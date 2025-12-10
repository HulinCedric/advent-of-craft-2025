using Xunit;

namespace TimePortal.Tests;

public class PortalNetworkTest
{
    private readonly PortalNetwork _network;

    public PortalNetworkTest()
    {
        _network = new PortalNetwork("Global Temporal Network");
    }

    #region Initial State

    [Fact(DisplayName = "Should have correct network name")]
    public void ShouldHaveCorrectNetworkName()
    {
        Assert.Equal("Global Temporal Network", _network.NetworkName);
    }

    [Fact(DisplayName = "Should start with no portals")]
    public void ShouldStartWithNoPortals()
    {
        Assert.Empty(_network.Portals);
    }

    [Fact(DisplayName = "Should have zero total energy initially")]
    public void ShouldHaveZeroTotalEnergy()
    {
        Assert.Equal(0, _network.GetTotalEnergy());
    }

    #endregion

    #region Portal Management

    [Fact(DisplayName = "Should add portals to the network")]
    public void ShouldAddPortal()
    {
        _network.AddPortal(new TimePortal("PORTAL-001"));
        _network.AddPortal(new TimePortal("PORTAL-002"));

        Assert.Equal(2, _network.Portals.Count);
    }

    #endregion

    #region Total Energy

    [Fact(DisplayName = "Should sum energy from all portals")]
    public void ShouldSumEnergyFromAllPortals()
    {
        var portal1 = new TimePortal("PORTAL-001");
        portal1.Charge(100);

        var portal2 = new TimePortal("PORTAL-002");
        portal2.Charge(150);

        _network.AddPortal(portal1);
        _network.AddPortal(portal2);

        Assert.Equal(250, _network.GetTotalEnergy());
    }

    #endregion

    #region Finding Active Portals

    [Fact(DisplayName = "Should return null when no suitable portal exists")]
    public void ShouldReturnNullWhenNoSuitablePortal()
    {
        Assert.Null(_network.FindActivePortalWithEnergy(100));
    }

    [Fact(DisplayName = "Should find active portal with sufficient energy")]
    public void ShouldFindActivePortalWithSufficientEnergy()
    {
        var portal = new TimePortal("PORTAL-001");
        portal.Activate();
        portal.Charge(200);
        _network.AddPortal(portal);

        var found = _network.FindActivePortalWithEnergy(100);

        Assert.NotNull(found);
    }

    [Fact(DisplayName = "Should not find inactive portals")]
    public void ShouldNotFindInactivePortals()
    {
        var inactivePortal = new TimePortal("INACTIVE");
        inactivePortal.Charge(500);
        _network.AddPortal(inactivePortal);

        Assert.Null(_network.FindActivePortalWithEnergy(100));
    }

    [Fact(DisplayName = "Should not find portals with insufficient energy")]
    public void ShouldNotFindPortalsWithInsufficientEnergy()
    {
        var lowEnergyPortal = new TimePortal("LOW-ENERGY");
        lowEnergyPortal.Activate();
        lowEnergyPortal.Charge(50);
        _network.AddPortal(lowEnergyPortal);

        Assert.Null(_network.FindActivePortalWithEnergy(100));
    }

    #endregion
}
