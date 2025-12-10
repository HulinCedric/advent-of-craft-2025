namespace TimePortal;

public class PortalNetwork
{
    private List<TimePortal> _portals;
    private string _networkName;

    public PortalNetwork(string networkName)
    {
        _networkName = networkName;
        _portals = new List<TimePortal>();
    }

    public void AddPortal(TimePortal portal)
    {
        _portals.Add(portal);
    }

    public int GetTotalEnergy()
    {
        var total = 0;
        for (var i = 0; i < _portals.Count; i++)
        {
            total = total + _portals[i].EnergyLevel;
        }
        return total;
    }

    public TimePortal? FindActivePortalWithEnergy(int minimumEnergy)
    {
        for (var i = 0; i < _portals.Count; i++)
        {
            if (_portals[i].IsActive && _portals[i].EnergyLevel >= minimumEnergy)
            {
                return _portals[i];
            }
        }
        return null;
    }

    public string NetworkName => _networkName;
    public List<TimePortal> Portals => _portals;
}
