package timeportal

import scala.collection.mutable.ListBuffer

class PortalNetwork(val networkName: String):
  private val portals: ListBuffer[TimePortal] = ListBuffer.empty

  def addPortal(portal: TimePortal): Unit = portals += portal

  def getTotalEnergy: Int =
    var total = 0
    for i <- portals.indices do
      total = total + portals(i).energyLevel
    total

  def findActivePortalWithEnergy(minimumEnergy: Int): Option[TimePortal] =
    for i <- portals.indices do
      if portals(i).isActive && portals(i).energyLevel >= minimumEnergy then
        return Some(portals(i))
    None

  def getPortals: List[TimePortal] = portals.toList
