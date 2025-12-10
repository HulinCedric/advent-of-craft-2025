package timeportal

import org.scalatest.BeforeAndAfterEach
import org.scalatest.funspec.AnyFunSpec
import org.scalatest.matchers.should.Matchers

class TimePortalTest extends AnyFunSpec with Matchers with BeforeAndAfterEach:

  var portal: TimePortal = _
  var traveler: Traveler = _

  override def beforeEach(): Unit =
    portal = TimePortal("PORTAL-001")
    traveler = Traveler("Dr. Elena Vance", "VISA-2024-001")

  describe("Activation"):

    it("should be inactive by default"):
      portal.isActive shouldBe false

    it("should be active after activation"):
      portal.activate()
      portal.isActive shouldBe true

    it("should be inactive after deactivation"):
      portal.activate()
      portal.deactivate()
      portal.isActive shouldBe false

  describe("Charging"):

    it("should start with zero energy"):
      portal.energyLevel shouldBe 0

    it("should increase energy when charged"):
      portal.charge(50)
      portal.energyLevel shouldBe 50

    it("should cap energy at maximum 500"):
      portal.charge(600)
      portal.energyLevel shouldBe 500

    it("should ignore non-positive charge amounts"):
      portal.charge(100)
      portal.charge(-50)
      portal.charge(0)
      portal.energyLevel shouldBe 100

  describe("Jump Initiation"):

    it("should succeed with valid parameters"):
      portal.activate()
      portal.charge(200)
      val result = portal.initiateJump(traveler, 1985, "COORD001")
      result should startWith("SUCCESS")

    it("should consume energy on successful jump"):
      portal.activate()
      portal.charge(200)
      val energyBefore = portal.energyLevel
      portal.initiateJump(traveler, 1985, "COORD001")
      portal.energyLevel should be < energyBefore

    it("should update traveler's current year"):
      portal.activate()
      portal.charge(200)
      portal.initiateJump(traveler, 1985, "COORD001")
      traveler.currentYear shouldBe 1985

    it("should fail when portal is inactive"):
      portal.charge(200)
      val result = portal.initiateJump(traveler, 1985, "COORD001")
      result should startWith("ERROR")

    it("should fail when energy is insufficient"):
      val lowEnergyPortal = TimePortal("LOW-ENERGY")
      lowEnergyPortal.activate()
      lowEnergyPortal.charge(50)
      val result = lowEnergyPortal.initiateJump(traveler, 1985, "COORD001")
      result should startWith("ERROR")

    it("should fail when destination year is out of range"):
      portal.activate()
      portal.charge(200)
      val result = portal.initiateJump(traveler, -15000, "COORD001")
      result should startWith("ERROR")

    it("should fail when coordinates are invalid"):
      portal.activate()
      portal.charge(200)
      portal.initiateJump(traveler, 1985, null) should startWith("ERROR")
      portal.initiateJump(traveler, 1985, "SHORT") should startWith("ERROR")

  describe("Travel History"):

    it("should record successful jumps"):
      portal.activate()
      portal.charge(200)
      portal.initiateJump(traveler, 1985, "COORD001")
      val history = portal.getTravelHistory("Dr. Elena Vance")
      history should not be empty

    it("should return empty for unknown traveler"):
      portal.activate()
      portal.charge(200)
      portal.initiateJump(traveler, 1985, "COORD001")
      val history = portal.getTravelHistory("Unknown Person")
      history shouldBe empty

  describe("Portal Properties"):

    it("should expose portal ID"):
      portal.portalId should not be null

    it("should expose status"):
      portal.currentStatus should not be null


class TravelerTest extends AnyFunSpec with Matchers with BeforeAndAfterEach:

  var traveler: Traveler = _

  override def beforeEach(): Unit =
    traveler = Traveler("Dr. Elena Vance", "VISA-2024-001")

  describe("Initial State"):

    it("should have correct name"):
      traveler.name shouldBe "Dr. Elena Vance"

    it("should have correct visa code"):
      traveler.visaCode shouldBe "VISA-2024-001"

    it("should start with zero jumps"):
      traveler.jumpCount shouldBe 0

    it("should start with STABLE health"):
      traveler.getHealthStatus shouldBe "STABLE"

    it("should not have temporal sickness initially"):
      traveler.hasTemporalSickness shouldBe false

  describe("Year Tracking"):

    it("should track current year"):
      traveler.setCurrentYear(1985)
      traveler.currentYear shouldBe 1985

  describe("Jump Count and Health"):

    it("should increment jump count"):
      traveler.incrementJumpCount()
      traveler.jumpCount shouldBe 1

    it("should degrade health after many jumps"):
      (1 to 6).foreach(_ => traveler.incrementJumpCount())
      traveler.getHealthStatus should not be "STABLE"

    it("should become critical after excessive jumps"):
      (1 to 11).foreach(_ => traveler.incrementJumpCount())
      traveler.getHealthStatus shouldBe "CRITICAL"

  describe("Temporal Sickness"):

    it("should have temporal sickness after excessive jumps"):
      (1 to 11).foreach(_ => traveler.incrementJumpCount())
      traveler.hasTemporalSickness shouldBe true

    it("should have temporal sickness immediately after a jump"):
      traveler.incrementJumpCount()
      traveler.hasTemporalSickness shouldBe true


class PortalNetworkTest extends AnyFunSpec with Matchers with BeforeAndAfterEach:

  var network: PortalNetwork = _

  override def beforeEach(): Unit =
    network = PortalNetwork("Global Temporal Network")

  describe("Initial State"):

    it("should have correct network name"):
      network.networkName shouldBe "Global Temporal Network"

    it("should start with no portals"):
      network.getPortals shouldBe empty

    it("should have zero total energy initially"):
      network.getTotalEnergy shouldBe 0

  describe("Portal Management"):

    it("should add portals to the network"):
      network.addPortal(TimePortal("PORTAL-001"))
      network.addPortal(TimePortal("PORTAL-002"))
      network.getPortals.size shouldBe 2

  describe("Total Energy"):

    it("should sum energy from all portals"):
      val portal1 = TimePortal("PORTAL-001")
      portal1.charge(100)
      val portal2 = TimePortal("PORTAL-002")
      portal2.charge(150)

      network.addPortal(portal1)
      network.addPortal(portal2)

      network.getTotalEnergy shouldBe 250

  describe("Finding Active Portals"):

    it("should return None when no suitable portal exists"):
      network.findActivePortalWithEnergy(100) shouldBe None

    it("should find active portal with sufficient energy"):
      val portal = TimePortal("PORTAL-001")
      portal.activate()
      portal.charge(200)
      network.addPortal(portal)

      network.findActivePortalWithEnergy(100) shouldBe defined

    it("should not find inactive portals"):
      val inactivePortal = TimePortal("INACTIVE")
      inactivePortal.charge(500)
      network.addPortal(inactivePortal)

      network.findActivePortalWithEnergy(100) shouldBe None

    it("should not find portals with insufficient energy"):
      val lowEnergyPortal = TimePortal("LOW-ENERGY")
      lowEnergyPortal.activate()
      lowEnergyPortal.charge(50)
      network.addPortal(lowEnergyPortal)

      network.findActivePortalWithEnergy(100) shouldBe None
