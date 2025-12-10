package timeportal;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

@DisplayName("Traveler")
class TravelerTest {

    private Traveler traveler;

    @BeforeEach
    void setUp() {
        traveler = new Traveler("Dr. Elena Vance", "VISA-2024-001");
    }

    @Nested
    @DisplayName("Initial State")
    class InitialState {

        @Test
        @DisplayName("should have correct name")
        void shouldHaveCorrectName() {
            assertEquals("Dr. Elena Vance", traveler.getName());
        }

        @Test
        @DisplayName("should have correct visa code")
        void shouldHaveCorrectVisaCode() {
            assertEquals("VISA-2024-001", traveler.getVisaCode());
        }

        @Test
        @DisplayName("should start with zero jumps")
        void shouldStartWithZeroJumps() {
            assertEquals(0, traveler.getJumpCount());
        }

        @Test
        @DisplayName("should start with STABLE health")
        void shouldStartWithStableHealth() {
            assertEquals("STABLE", traveler.getHealthStatus());
        }

        @Test
        @DisplayName("should not have temporal sickness initially")
        void shouldNotHaveTemporalSicknessInitially() {
            assertFalse(traveler.hasTemporalSickness());
        }
    }

    @Nested
    @DisplayName("Year Tracking")
    class YearTracking {

        @Test
        @DisplayName("should track current year")
        void shouldTrackCurrentYear() {
            traveler.setCurrentYear(1985);
            assertEquals(1985, traveler.getCurrentYear());
        }
    }

    @Nested
    @DisplayName("Jump Count and Health")
    class JumpCountAndHealth {

        @Test
        @DisplayName("should increment jump count")
        void shouldIncrementJumpCount() {
            traveler.incrementJumpCount();
            assertEquals(1, traveler.getJumpCount());
        }

        @Test
        @DisplayName("should degrade health after many jumps")
        void shouldDegradeHealthAfterManyJumps() {
            for (int i = 0; i < 6; i++) {
                traveler.incrementJumpCount();
            }
            assertNotEquals("STABLE", traveler.getHealthStatus());
        }

        @Test
        @DisplayName("should become critical after excessive jumps")
        void shouldBecomeCriticalAfterExcessiveJumps() {
            for (int i = 0; i < 11; i++) {
                traveler.incrementJumpCount();
            }
            assertEquals("CRITICAL", traveler.getHealthStatus());
        }
    }

    @Nested
    @DisplayName("Temporal Sickness")
    class TemporalSickness {

        @Test
        @DisplayName("should have temporal sickness after excessive jumps")
        void shouldHaveTemporalSicknessAfterManyJumps() {
            for (int i = 0; i < 11; i++) {
                traveler.incrementJumpCount();
            }
            assertTrue(traveler.hasTemporalSickness());
        }

        @Test
        @DisplayName("should have temporal sickness immediately after a jump")
        void shouldHaveTemporalSicknessRightAfterJump() {
            traveler.incrementJumpCount();
            assertTrue(traveler.hasTemporalSickness());
        }
    }
}
