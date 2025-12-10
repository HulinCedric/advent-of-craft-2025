package timeportal

class TimePortal(val portalId: String) {
    var energyLevel: Int = 0
        private set
    private val travelLog: MutableList<String> = mutableListOf()
    var isActive: Boolean = false
        private set
    var currentStatus: String = "IDLE" // "IDLE", "CHARGING", "JUMPING", "ERROR"
        private set

    fun initiateJump(traveler: Traveler, destinationYear: Int, coordinates: String?): String {
        if (isActive) {
            if (energyLevel >= 100) {
                if (destinationYear >= -10000 && destinationYear <= 3000) {
                    if (coordinates != null && coordinates.length == 8) {
                        if (traveler.getHealthStatus() == "STABLE") {
                            if (!traveler.hasTemporalSickness()) {
                                currentStatus = "JUMPING"
                                energyLevel -= 100
                                val logEntry = "${traveler.name}|$destinationYear|$coordinates|${System.currentTimeMillis()}"
                                travelLog.add(logEntry)
                                traveler.setCurrentYear(destinationYear)
                                traveler.incrementJumpCount()
                                currentStatus = "IDLE"
                                return "SUCCESS: ${traveler.name} transported to year $destinationYear"
                            } else {
                                return "ERROR: Traveler has temporal sickness"
                            }
                        } else {
                            return "ERROR: Traveler health unstable"
                        }
                    } else {
                        return "ERROR: Invalid coordinates format"
                    }
                } else {
                    return "ERROR: Destination year out of range"
                }
            } else {
                return "ERROR: Insufficient energy. Current: $energyLevel"
            }
        } else {
            return "ERROR: Portal is not active"
        }
    }

    fun charge(amount: Int) {
        if (amount > 0) {
            if (currentStatus == "IDLE" || currentStatus == "CHARGING") {
                currentStatus = "CHARGING"
                energyLevel += amount
                if (energyLevel > 500) {
                    energyLevel = 500
                }
                currentStatus = "IDLE"
            }
        }
    }

    fun getTravelHistory(travelerName: String): String {
        var result = ""
        for (i in travelLog.indices) {
            val parts = travelLog[i].split("|")
            if (parts[0] == travelerName) {
                result = result + "Year: ${parts[1]}, Coords: ${parts[2]}\n"
            }
        }
        return result
    }

    fun activate() {
        isActive = true
    }

    fun deactivate() {
        isActive = false
    }

    fun getTravelLog(): List<String> = travelLog
}
