package timeportal

import scala.collection.mutable.ListBuffer

class TimePortal(val portalId: String):
  private var _energyLevel: Int = 0
  private val travelLog: ListBuffer[String] = ListBuffer.empty
  private var _isActive: Boolean = false
  private var _currentStatus: String = "IDLE" // "IDLE", "CHARGING", "JUMPING", "ERROR"

  def energyLevel: Int = _energyLevel
  def isActive: Boolean = _isActive
  def currentStatus: String = _currentStatus

  def initiateJump(traveler: Traveler, destinationYear: Int, coordinates: String): String =
    if _isActive then
      if _energyLevel >= 100 then
        if destinationYear >= -10000 && destinationYear <= 3000 then
          if coordinates != null && coordinates.length == 8 then
            if traveler.getHealthStatus == "STABLE" then
              if !traveler.hasTemporalSickness then
                _currentStatus = "JUMPING"
                _energyLevel = _energyLevel - 100
                val logEntry = s"${traveler.name}|$destinationYear|$coordinates|${System.currentTimeMillis()}"
                travelLog += logEntry
                traveler.setCurrentYear(destinationYear)
                traveler.incrementJumpCount()
                _currentStatus = "IDLE"
                s"SUCCESS: ${traveler.name} transported to year $destinationYear"
              else
                "ERROR: Traveler has temporal sickness"
            else
              "ERROR: Traveler health unstable"
          else
            "ERROR: Invalid coordinates format"
        else
          "ERROR: Destination year out of range"
      else
        s"ERROR: Insufficient energy. Current: $_energyLevel"
    else
      "ERROR: Portal is not active"

  def charge(amount: Int): Unit =
    if amount > 0 then
      if _currentStatus == "IDLE" || _currentStatus == "CHARGING" then
        _currentStatus = "CHARGING"
        _energyLevel = _energyLevel + amount
        if _energyLevel > 500 then
          _energyLevel = 500
        _currentStatus = "IDLE"

  def getTravelHistory(travelerName: String): String =
    var result = ""
    for i <- travelLog.indices do
      val parts = travelLog(i).split("\\|")
      if parts(0) == travelerName then
        result = result + s"Year: ${parts(1)}, Coords: ${parts(2)}\n"
    result

  def activate(): Unit = _isActive = true
  def deactivate(): Unit = _isActive = false
  def getTravelLog: List[String] = travelLog.toList
