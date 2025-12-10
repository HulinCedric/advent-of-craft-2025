package timeportal

class Traveler(val name: String, val visaCode: String):
  private var _currentYear: Int = 2024
  private var _jumpCount: Int = 0
  private var _healthStatus: String = "STABLE" // "STABLE", "UNSTABLE", "CRITICAL"
  private var lastJumpTimestamp: Long = 0

  def currentYear: Int = _currentYear
  def jumpCount: Int = _jumpCount

  def hasTemporalSickness: Boolean =
    if _jumpCount > 10 then
      true
    else
      if System.currentTimeMillis() - lastJumpTimestamp < 3600000 then
        true
      else
        false

  def incrementJumpCount(): Unit =
    _jumpCount += 1
    lastJumpTimestamp = System.currentTimeMillis()
    if _jumpCount > 5 then
      _healthStatus = "UNSTABLE"
    if _jumpCount > 10 then
      _healthStatus = "CRITICAL"

  def setCurrentYear(year: Int): Unit = _currentYear = year
  def getHealthStatus: String = _healthStatus
