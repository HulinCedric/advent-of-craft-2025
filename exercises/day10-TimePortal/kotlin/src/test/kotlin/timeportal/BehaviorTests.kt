package timeportal

import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.DisplayName
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFalse
import kotlin.test.assertNotNull
import kotlin.test.assertNull
import kotlin.test.assertTrue

@DisplayName("TimePortal")
class TimePortalTest {

    private lateinit var portal: TimePortal
    private lateinit var traveler: Traveler

    @BeforeEach
    fun setUp() {
        portal = TimePortal("PORTAL-001")
        traveler = Traveler("Dr. Elena Vance", "VISA-2024-001")
    }

    @Nested
    @DisplayName("Activation")
    inner class Activation {

        @Test
        @DisplayName("should be inactive by default")
        fun shouldBeInactiveByDefault() {
            assertFalse(portal.isActive)
        }

        @Test
        @DisplayName("should be active after activation")
        fun shouldBeActiveAfterActivation() {
            portal.activate()
            assertTrue(portal.isActive)
        }

        @Test
        @DisplayName("should be inactive after deactivation")
        fun shouldBeInactiveAfterDeactivation() {
            portal.activate()
            portal.deactivate()
            assertFalse(portal.isActive)
        }
    }

    @Nested
    @DisplayName("Charging")
    inner class Charging {

        @Test
        @DisplayName("should start with zero energy")
        fun shouldStartWithZeroEnergy() {
            assertEquals(0, portal.energyLevel)
        }

        @Test
        @DisplayName("should increase energy when charged")
        fun shouldIncreaseEnergyWhenCharged() {
            portal.charge(50)
            assertEquals(50, portal.energyLevel)
        }

        @Test
        @DisplayName("should cap energy at maximum 500")
        fun shouldCapEnergyAtMaximum() {
            portal.charge(600)
            assertEquals(500, portal.energyLevel)
        }

        @Test
        @DisplayName("should ignore non-positive charge amounts")
        fun shouldIgnoreNonPositiveCharge() {
            portal.charge(100)
            portal.charge(-50)
            portal.charge(0)
            assertEquals(100, portal.energyLevel)
        }
    }

    @Nested
    @DisplayName("Jump Initiation")
    inner class JumpInitiation {

        @BeforeEach
        fun preparePortal() {
            portal.activate()
            portal.charge(200)
        }

        @Test
        @DisplayName("should succeed with valid parameters")
        fun shouldSucceedWithValidParameters() {
            val result = portal.initiateJump(traveler, 1985, "COORD001")
            assertTrue(result.startsWith("SUCCESS"))
        }

        @Test
        @DisplayName("should consume energy on successful jump")
        fun shouldConsumeEnergyOnSuccess() {
            val energyBefore = portal.energyLevel
            portal.initiateJump(traveler, 1985, "COORD001")
            assertTrue(portal.energyLevel < energyBefore)
        }

        @Test
        @DisplayName("should update traveler's current year")
        fun shouldUpdateTravelerYear() {
            portal.initiateJump(traveler, 1985, "COORD001")
            assertEquals(1985, traveler.currentYear)
        }

        @Test
        @DisplayName("should fail when portal is inactive")
        fun shouldFailWhenInactive() {
            portal.deactivate()
            val result = portal.initiateJump(traveler, 1985, "COORD001")
            assertTrue(result.startsWith("ERROR"))
        }

        @Test
        @DisplayName("should fail when energy is insufficient")
        fun shouldFailWhenInsufficientEnergy() {
            val lowEnergyPortal = TimePortal("LOW-ENERGY")
            lowEnergyPortal.activate()
            lowEnergyPortal.charge(50)

            val result = lowEnergyPortal.initiateJump(traveler, 1985, "COORD001")
            assertTrue(result.startsWith("ERROR"))
        }

        @Test
        @DisplayName("should fail when destination year is out of range")
        fun shouldFailWhenYearOutOfRange() {
            val result = portal.initiateJump(traveler, -15000, "COORD001")
            assertTrue(result.startsWith("ERROR"))
        }

        @Test
        @DisplayName("should fail when coordinates are invalid")
        fun shouldFailWhenCoordinatesInvalid() {
            val nullCoords = portal.initiateJump(traveler, 1985, null)
            assertTrue(nullCoords.startsWith("ERROR"))

            val shortCoords = portal.initiateJump(traveler, 1985, "SHORT")
            assertTrue(shortCoords.startsWith("ERROR"))
        }
    }

    @Nested
    @DisplayName("Travel History")
    inner class TravelHistory {

        @Test
        @DisplayName("should record successful jumps")
        fun shouldRecordJump() {
            portal.activate()
            portal.charge(200)
            portal.initiateJump(traveler, 1985, "COORD001")

            val history = portal.getTravelHistory("Dr. Elena Vance")
            assertTrue(history.isNotEmpty())
        }

        @Test
        @DisplayName("should return empty for unknown traveler")
        fun shouldReturnEmptyForUnknownTraveler() {
            portal.activate()
            portal.charge(200)
            portal.initiateJump(traveler, 1985, "COORD001")

            val history = portal.getTravelHistory("Unknown Person")
            assertTrue(history.isEmpty())
        }
    }

    @Nested
    @DisplayName("Portal Properties")
    inner class PortalProperties {

        @Test
        @DisplayName("should expose portal ID")
        fun shouldExposePortalId() {
            assertNotNull(portal.portalId)
        }

        @Test
        @DisplayName("should expose status")
        fun shouldExposeStatus() {
            assertNotNull(portal.currentStatus)
        }
    }
}

@DisplayName("Traveler")
class TravelerTest {

    private lateinit var traveler: Traveler

    @BeforeEach
    fun setUp() {
        traveler = Traveler("Dr. Elena Vance", "VISA-2024-001")
    }

    @Nested
    @DisplayName("Initial State")
    inner class InitialState {

        @Test
        @DisplayName("should have correct name")
        fun shouldHaveCorrectName() {
            assertEquals("Dr. Elena Vance", traveler.name)
        }

        @Test
        @DisplayName("should have correct visa code")
        fun shouldHaveCorrectVisaCode() {
            assertEquals("VISA-2024-001", traveler.visaCode)
        }

        @Test
        @DisplayName("should start with zero jumps")
        fun shouldStartWithZeroJumps() {
            assertEquals(0, traveler.jumpCount)
        }

        @Test
        @DisplayName("should start with STABLE health")
        fun shouldStartWithStableHealth() {
            assertEquals("STABLE", traveler.getHealthStatus())
        }

        @Test
        @DisplayName("should not have temporal sickness initially")
        fun shouldNotHaveTemporalSicknessInitially() {
            assertFalse(traveler.hasTemporalSickness())
        }
    }

    @Nested
    @DisplayName("Year Tracking")
    inner class YearTracking {

        @Test
        @DisplayName("should track current year")
        fun shouldTrackCurrentYear() {
            traveler.setCurrentYear(1985)
            assertEquals(1985, traveler.currentYear)
        }
    }

    @Nested
    @DisplayName("Jump Count and Health")
    inner class JumpCountAndHealth {

        @Test
        @DisplayName("should increment jump count")
        fun shouldIncrementJumpCount() {
            traveler.incrementJumpCount()
            assertEquals(1, traveler.jumpCount)
        }

        @Test
        @DisplayName("should degrade health after many jumps")
        fun shouldDegradeHealthAfterManyJumps() {
            repeat(6) { traveler.incrementJumpCount() }
            assertTrue(traveler.getHealthStatus() != "STABLE")
        }

        @Test
        @DisplayName("should become critical after excessive jumps")
        fun shouldBecomeCriticalAfterExcessiveJumps() {
            repeat(11) { traveler.incrementJumpCount() }
            assertEquals("CRITICAL", traveler.getHealthStatus())
        }
    }

    @Nested
    @DisplayName("Temporal Sickness")
    inner class TemporalSickness {

        @Test
        @DisplayName("should have temporal sickness after excessive jumps")
        fun shouldHaveTemporalSicknessAfterManyJumps() {
            repeat(11) { traveler.incrementJumpCount() }
            assertTrue(traveler.hasTemporalSickness())
        }

        @Test
        @DisplayName("should have temporal sickness immediately after a jump")
        fun shouldHaveTemporalSicknessRightAfterJump() {
            traveler.incrementJumpCount()
            assertTrue(traveler.hasTemporalSickness())
        }
    }
}

@DisplayName("PortalNetwork")
class PortalNetworkTest {

    private lateinit var network: PortalNetwork

    @BeforeEach
    fun setUp() {
        network = PortalNetwork("Global Temporal Network")
    }

    @Nested
    @DisplayName("Initial State")
    inner class InitialState {

        @Test
        @DisplayName("should have correct network name")
        fun shouldHaveCorrectNetworkName() {
            assertEquals("Global Temporal Network", network.networkName)
        }

        @Test
        @DisplayName("should start with no portals")
        fun shouldStartWithNoPortals() {
            assertTrue(network.getPortals().isEmpty())
        }

        @Test
        @DisplayName("should have zero total energy initially")
        fun shouldHaveZeroTotalEnergy() {
            assertEquals(0, network.getTotalEnergy())
        }
    }

    @Nested
    @DisplayName("Portal Management")
    inner class PortalManagement {

        @Test
        @DisplayName("should add portals to the network")
        fun shouldAddPortal() {
            network.addPortal(TimePortal("PORTAL-001"))
            network.addPortal(TimePortal("PORTAL-002"))

            assertEquals(2, network.getPortals().size)
        }
    }

    @Nested
    @DisplayName("Total Energy")
    inner class TotalEnergy {

        @Test
        @DisplayName("should sum energy from all portals")
        fun shouldSumEnergyFromAllPortals() {
            val portal1 = TimePortal("PORTAL-001").apply { charge(100) }
            val portal2 = TimePortal("PORTAL-002").apply { charge(150) }

            network.addPortal(portal1)
            network.addPortal(portal2)

            assertEquals(250, network.getTotalEnergy())
        }
    }

    @Nested
    @DisplayName("Finding Active Portals")
    inner class FindingActivePortals {

        @Test
        @DisplayName("should return null when no suitable portal exists")
        fun shouldReturnNullWhenNoSuitablePortal() {
            assertNull(network.findActivePortalWithEnergy(100))
        }

        @Test
        @DisplayName("should find active portal with sufficient energy")
        fun shouldFindActivePortalWithSufficientEnergy() {
            val portal = TimePortal("PORTAL-001").apply {
                activate()
                charge(200)
            }
            network.addPortal(portal)

            assertNotNull(network.findActivePortalWithEnergy(100))
        }

        @Test
        @DisplayName("should not find inactive portals")
        fun shouldNotFindInactivePortals() {
            val inactivePortal = TimePortal("INACTIVE").apply { charge(500) }
            network.addPortal(inactivePortal)

            assertNull(network.findActivePortalWithEnergy(100))
        }

        @Test
        @DisplayName("should not find portals with insufficient energy")
        fun shouldNotFindPortalsWithInsufficientEnergy() {
            val lowEnergyPortal = TimePortal("LOW-ENERGY").apply {
                activate()
                charge(50)
            }
            network.addPortal(lowEnergyPortal)

            assertNull(network.findActivePortalWithEnergy(100))
        }
    }
}
