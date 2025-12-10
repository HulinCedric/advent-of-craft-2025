package timeportal;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

@DisplayName("PortalNetwork")
class PortalNetworkTest {

    private PortalNetwork network;

    @BeforeEach
    void setUp() {
        network = new PortalNetwork("Global Temporal Network");
    }

    @Nested
    @DisplayName("Initial State")
    class InitialState {

        @Test
        @DisplayName("should have correct network name")
        void shouldHaveCorrectNetworkName() {
            assertEquals("Global Temporal Network", network.getNetworkName());
        }

        @Test
        @DisplayName("should start with no portals")
        void shouldStartWithNoPortals() {
            assertTrue(network.getPortals().isEmpty());
        }

        @Test
        @DisplayName("should have zero total energy initially")
        void shouldHaveZeroTotalEnergy() {
            assertEquals(0, network.getTotalEnergy());
        }
    }

    @Nested
    @DisplayName("Portal Management")
    class PortalManagement {

        @Test
        @DisplayName("should add portals to the network")
        void shouldAddPortal() {
            network.addPortal(new TimePortal("PORTAL-001"));
            network.addPortal(new TimePortal("PORTAL-002"));

            assertEquals(2, network.getPortals().size());
        }
    }

    @Nested
    @DisplayName("Total Energy")
    class TotalEnergy {

        @Test
        @DisplayName("should sum energy from all portals")
        void shouldSumEnergyFromAllPortals() {
            TimePortal portal1 = new TimePortal("PORTAL-001");
            portal1.charge(100);

            TimePortal portal2 = new TimePortal("PORTAL-002");
            portal2.charge(150);

            network.addPortal(portal1);
            network.addPortal(portal2);

            assertEquals(250, network.getTotalEnergy());
        }
    }

    @Nested
    @DisplayName("Finding Active Portals")
    class FindingActivePortals {

        @Test
        @DisplayName("should return null when no suitable portal exists")
        void shouldReturnNullWhenNoSuitablePortal() {
            assertNull(network.findActivePortalWithEnergy(100));
        }

        @Test
        @DisplayName("should find active portal with sufficient energy")
        void shouldFindActivePortalWithSufficientEnergy() {
            TimePortal portal = new TimePortal("PORTAL-001");
            portal.activate();
            portal.charge(200);
            network.addPortal(portal);

            TimePortal found = network.findActivePortalWithEnergy(100);
            assertNotNull(found);
        }

        @Test
        @DisplayName("should not find inactive portals")
        void shouldNotFindInactivePortals() {
            TimePortal inactivePortal = new TimePortal("INACTIVE");
            inactivePortal.charge(500);
            network.addPortal(inactivePortal);

            assertNull(network.findActivePortalWithEnergy(100));
        }

        @Test
        @DisplayName("should not find portals with insufficient energy")
        void shouldNotFindPortalsWithInsufficientEnergy() {
            TimePortal lowEnergyPortal = new TimePortal("LOW-ENERGY");
            lowEnergyPortal.activate();
            lowEnergyPortal.charge(50);
            network.addPortal(lowEnergyPortal);

            assertNull(network.findActivePortalWithEnergy(100));
        }
    }
}
