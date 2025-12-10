namespace TimePortal;

public class Traveler
{
    private string _name;
    private string _visaCode;
    private int _currentYear;
    private int _jumpCount;
    private string _healthStatus; // "STABLE", "UNSTABLE", "CRITICAL"
    private long _lastJumpTimestamp;

    public Traveler(string name, string visaCode)
    {
        _name = name;
        _visaCode = visaCode;
        _currentYear = 2024;
        _jumpCount = 0;
        _healthStatus = "STABLE";
        _lastJumpTimestamp = 0;
    }

    public bool HasTemporalSickness()
    {
        if (_jumpCount > 10)
        {
            return true;
        }
        else
        {
            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _lastJumpTimestamp < 3600000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void IncrementJumpCount()
    {
        _jumpCount++;
        _lastJumpTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        if (_jumpCount > 5)
        {
            _healthStatus = "UNSTABLE";
        }
        if (_jumpCount > 10)
        {
            _healthStatus = "CRITICAL";
        }
    }

    public string Name => _name;
    public string VisaCode => _visaCode;
    public int CurrentYear => _currentYear;
    public void SetCurrentYear(int year) => _currentYear = year;
    public string GetHealthStatus() => _healthStatus;
    public int JumpCount => _jumpCount;
}
