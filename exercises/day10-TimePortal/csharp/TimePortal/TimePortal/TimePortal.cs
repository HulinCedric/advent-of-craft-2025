namespace TimePortal;

public class TimePortal
{
    private string _portalId;
    private int _energyLevel;
    private List<string> _travelLog;
    private bool _isActive;
    private string _currentStatus; // "IDLE", "CHARGING", "JUMPING", "ERROR"

    public TimePortal(string portalId)
    {
        _portalId = portalId;
        _energyLevel = 0;
        _travelLog = new List<string>();
        _isActive = false;
        _currentStatus = "IDLE";
    }

    public string InitiateJump(Traveler traveler, int destinationYear, string coordinates)
    {
        if (_isActive)
        {
            if (_energyLevel >= 100)
            {
                if (destinationYear >= -10000 && destinationYear <= 3000)
                {
                    if (coordinates != null && coordinates.Length == 8)
                    {
                        if (traveler.GetHealthStatus().Equals("STABLE"))
                        {
                            if (!traveler.HasTemporalSickness())
                            {
                                _currentStatus = "JUMPING";
                                _energyLevel = _energyLevel - 100;
                                var logEntry = $"{traveler.Name}|{destinationYear}|{coordinates}|{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
                                _travelLog.Add(logEntry);
                                traveler.SetCurrentYear(destinationYear);
                                traveler.IncrementJumpCount();
                                _currentStatus = "IDLE";
                                return $"SUCCESS: {traveler.Name} transported to year {destinationYear}";
                            }
                            else
                            {
                                return "ERROR: Traveler has temporal sickness";
                            }
                        }
                        else
                        {
                            return "ERROR: Traveler health unstable";
                        }
                    }
                    else
                    {
                        return "ERROR: Invalid coordinates format";
                    }
                }
                else
                {
                    return "ERROR: Destination year out of range";
                }
            }
            else
            {
                return $"ERROR: Insufficient energy. Current: {_energyLevel}";
            }
        }
        else
        {
            return "ERROR: Portal is not active";
        }
    }

    public void Charge(int amount)
    {
        if (amount > 0)
        {
            if (_currentStatus.Equals("IDLE") || _currentStatus.Equals("CHARGING"))
            {
                _currentStatus = "CHARGING";
                _energyLevel = _energyLevel + amount;
                if (_energyLevel > 500)
                {
                    _energyLevel = 500;
                }
                _currentStatus = "IDLE";
            }
        }
    }

    public string GetTravelHistory(string travelerName)
    {
        var result = "";
        for (var i = 0; i < _travelLog.Count; i++)
        {
            var parts = _travelLog[i].Split('|');
            if (parts[0].Equals(travelerName))
            {
                result = result + $"Year: {parts[1]}, Coords: {parts[2]}\n";
            }
        }
        return result;
    }

    public string PortalId => _portalId;
    public int EnergyLevel => _energyLevel;
    public bool IsActive => _isActive;
    public void Activate() => _isActive = true;
    public void Deactivate() => _isActive = false;
    public string Status => _currentStatus;
    public List<string> TravelLog => _travelLog;
}
