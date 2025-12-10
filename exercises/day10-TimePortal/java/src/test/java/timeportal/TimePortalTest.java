package timeportal;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

@DisplayName("TimePortal")
class TimePortalTest {

    private TimePortal portal;
    private Traveler traveler;

    @BeforeEach
    void setUp() {
        portal = new TimePortal("PORTAL-001");
        traveler = new Traveler("Dr. Elena Vance", "VISA-2024-001");
    }

    @Nested
    @DisplayName("Activation")
    class Activation {

        @Test
        @DisplayName("should be inactive by default")
        void shouldBeInactiveByDefault() {
            assertFalse(portal.isActive());
        }

        @Test
        @DisplayName("should be active after activation")
        void shouldBeActiveAfterActivation() {
            portal.activate();
            assertTrue(portal.isActive());
        }

        @Test
        @DisplayName("should be inactive after deactivation")
        void shouldBeInactiveAfterDeactivation() {
            portal.activate();
            portal.deactivate();
            assertFalse(portal.isActive());
        }
    }

    @Nested
    @DisplayName("Charging")
    class Charging {

        @Test
        @DisplayName("should start with zero energy")
        void shouldStartWithZeroEnergy() {
            assertEquals(0, portal.getEnergyLevel());
        }

        @Test
        @DisplayName("should increase energy when charged")
        void shouldIncreaseEnergyWhenCharged() {
            portal.charge(50);
            assertEquals(50, portal.getEnergyLevel());
        }

        @Test
        @DisplayName("should cap energy at maximum 500")
        void shouldCapEnergyAtMaximum() {
            portal.charge(600);
            assertEquals(500, portal.getEnergyLevel());
        }

        @Test
        @DisplayName("should ignore non-positive charge amounts")
        void shouldIgnoreNonPositiveCharge() {
            portal.charge(100);
            portal.charge(-50);
            portal.charge(0);
            assertEquals(100, portal.getEnergyLevel());
        }
    }

    @Nested
    @DisplayName("Jump Initiation")
    class JumpInitiation {

        @BeforeEach
        void preparePortal() {
            portal.activate();
            portal.charge(200);
        }

        @Test
        @DisplayName("should succeed with valid parameters")
        void shouldSucceedWithValidParameters() {
            String result = portal.initiateJump(traveler, 1985, "COORD001");
            assertTrue(result.startsWith("SUCCESS"));
        }

        @Test
        @DisplayName("should consume energy on successful jump")
        void shouldConsumeEnergyOnSuccess() {
            int energyBefore = portal.getEnergyLevel();
            portal.initiateJump(traveler, 1985, "COORD001");
            assertTrue(portal.getEnergyLevel() < energyBefore);
        }

        @Test
        @DisplayName("should update traveler's current year")
        void shouldUpdateTravelerYear() {
            portal.initiateJump(traveler, 1985, "COORD001");
            assertEquals(1985, traveler.getCurrentYear());
        }

        @Test
        @DisplayName("should fail when portal is inactive")
        void shouldFailWhenInactive() {
            portal.deactivate();
            String result = portal.initiateJump(traveler, 1985, "COORD001");
            assertTrue(result.startsWith("ERROR"));
        }

        @Test
        @DisplayName("should fail when energy is insufficient")
        void shouldFailWhenInsufficientEnergy() {
            TimePortal lowEnergyPortal = new TimePortal("LOW-ENERGY");
            lowEnergyPortal.activate();
            lowEnergyPortal.charge(50);

            String result = lowEnergyPortal.initiateJump(traveler, 1985, "COORD001");
            assertTrue(result.startsWith("ERROR"));
        }

        @Test
        @DisplayName("should fail when destination year is out of range")
        void shouldFailWhenYearOutOfRange() {
            String tooFarPast = portal.initiateJump(traveler, -15000, "COORD001");
            assertTrue(tooFarPast.startsWith("ERROR"));
        }

        @Test
        @DisplayName("should fail when coordinates are invalid")
        void shouldFailWhenCoordinatesInvalid() {
            String nullCoords = portal.initiateJump(traveler, 1985, null);
            assertTrue(nullCoords.startsWith("ERROR"));

            String shortCoords = portal.initiateJump(traveler, 1985, "SHORT");
            assertTrue(shortCoords.startsWith("ERROR"));
        }
    }

    @Nested
    @DisplayName("Travel History")
    class TravelHistory {

        @Test
        @DisplayName("should record successful jumps")
        void shouldRecordJump() {
            portal.activate();
            portal.charge(200);
            portal.initiateJump(traveler, 1985, "COORD001");

            String history = portal.getTravelHistory("Dr. Elena Vance");
            assertFalse(history.isEmpty());
        }

        @Test
        @DisplayName("should return empty for unknown traveler")
        void shouldReturnEmptyForUnknownTraveler() {
            portal.activate();
            portal.charge(200);
            portal.initiateJump(traveler, 1985, "COORD001");

            String history = portal.getTravelHistory("Unknown Person");
            assertTrue(history.isEmpty());
        }
    }

    @Nested
    @DisplayName("Portal Properties")
    class PortalProperties {

        @Test
        @DisplayName("should expose portal ID")
        void shouldExposePortalId() {
            assertNotNull(portal.getPortalId());
        }

        @Test
        @DisplayName("should expose status")
        void shouldExposeStatus() {
            assertNotNull(portal.getStatus());
        }
    }
}
