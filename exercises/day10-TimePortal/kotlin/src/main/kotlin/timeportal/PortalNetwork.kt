package timeportal

class PortalNetwork(val networkName: String) {
    private val portals: MutableList<TimePortal> = mutableListOf()

    fun addPortal(portal: TimePortal) {
        portals.add(portal)
    }

    fun getTotalEnergy(): Int {
        var total = 0
        for (i in portals.indices) {
            total += portals[i].energyLevel
        }
        return total
    }

    fun findActivePortalWithEnergy(minimumEnergy: Int): TimePortal? {
        for (i in portals.indices) {
            if (portals[i].isActive && portals[i].energyLevel >= minimumEnergy) {
                return portals[i]
            }
        }
        return null
    }

    fun getPortals(): List<TimePortal> = portals
}
