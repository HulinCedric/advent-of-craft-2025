package timeportal

class Traveler(val name: String, val visaCode: String) {
    var currentYear: Int = 2024
        private set
    var jumpCount: Int = 0
        private set
    private var healthStatus: String = "STABLE" // "STABLE", "UNSTABLE", "CRITICAL"
    private var lastJumpTimestamp: Long = 0

    fun hasTemporalSickness(): Boolean {
        if (jumpCount > 10) {
            return true
        } else {
            if (System.currentTimeMillis() - lastJumpTimestamp < 3600000) {
                return true
            } else {
                return false
            }
        }
    }

    fun incrementJumpCount() {
        jumpCount++
        lastJumpTimestamp = System.currentTimeMillis()
        if (jumpCount > 5) {
            healthStatus = "UNSTABLE"
        }
        if (jumpCount > 10) {
            healthStatus = "CRITICAL"
        }
    }

    fun setCurrentYear(year: Int) {
        currentYear = year
    }

    fun getHealthStatus(): String = healthStatus
}
