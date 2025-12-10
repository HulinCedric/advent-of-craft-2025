import { TimePortal } from '../src/TimePortal';
import { Traveler } from '../src/Traveler';
import { PortalNetwork } from '../src/PortalNetwork';

describe('TimePortal', () => {
    let portal: TimePortal;
    let traveler: Traveler;

    beforeEach(() => {
        portal = new TimePortal('PORTAL-001');
        traveler = new Traveler('Dr. Elena Vance', 'VISA-2024-001');
    });

    describe('Activation', () => {
        test('should be inactive by default', () => {
            expect(portal.isActive).toBe(false);
        });

        test('should be active after activation', () => {
            portal.activate();
            expect(portal.isActive).toBe(true);
        });

        test('should be inactive after deactivation', () => {
            portal.activate();
            portal.deactivate();
            expect(portal.isActive).toBe(false);
        });
    });

    describe('Charging', () => {
        test('should start with zero energy', () => {
            expect(portal.energyLevel).toBe(0);
        });

        test('should increase energy when charged', () => {
            portal.charge(50);
            expect(portal.energyLevel).toBe(50);
        });

        test('should cap energy at maximum 500', () => {
            portal.charge(600);
            expect(portal.energyLevel).toBe(500);
        });

        test('should ignore non-positive charge amounts', () => {
            portal.charge(100);
            portal.charge(-50);
            portal.charge(0);
            expect(portal.energyLevel).toBe(100);
        });
    });

    describe('Jump Initiation', () => {
        beforeEach(() => {
            portal.activate();
            portal.charge(200);
        });

        test('should succeed with valid parameters', () => {
            const result = portal.initiateJump(traveler, 1985, 'COORD001');
            expect(result).toMatch(/^SUCCESS/);
        });

        test('should consume energy on successful jump', () => {
            const energyBefore = portal.energyLevel;
            portal.initiateJump(traveler, 1985, 'COORD001');
            expect(portal.energyLevel).toBeLessThan(energyBefore);
        });

        test('should update traveler current year', () => {
            portal.initiateJump(traveler, 1985, 'COORD001');
            expect(traveler.currentYear).toBe(1985);
        });

        test('should fail when portal is inactive', () => {
            portal.deactivate();
            const result = portal.initiateJump(traveler, 1985, 'COORD001');
            expect(result).toMatch(/^ERROR/);
        });

        test('should fail when energy is insufficient', () => {
            const lowEnergyPortal = new TimePortal('LOW-ENERGY');
            lowEnergyPortal.activate();
            lowEnergyPortal.charge(50);

            const result = lowEnergyPortal.initiateJump(traveler, 1985, 'COORD001');
            expect(result).toMatch(/^ERROR/);
        });

        test('should fail when destination year is out of range', () => {
            const result = portal.initiateJump(traveler, -15000, 'COORD001');
            expect(result).toMatch(/^ERROR/);
        });

        test('should fail when coordinates are invalid', () => {
            const nullResult = portal.initiateJump(traveler, 1985, null);
            expect(nullResult).toMatch(/^ERROR/);

            const shortResult = portal.initiateJump(traveler, 1985, 'SHORT');
            expect(shortResult).toMatch(/^ERROR/);
        });
    });

    describe('Travel History', () => {
        test('should record successful jumps', () => {
            portal.activate();
            portal.charge(200);
            portal.initiateJump(traveler, 1985, 'COORD001');

            const history = portal.getTravelHistory('Dr. Elena Vance');
            expect(history.length).toBeGreaterThan(0);
        });

        test('should return empty for unknown traveler', () => {
            portal.activate();
            portal.charge(200);
            portal.initiateJump(traveler, 1985, 'COORD001');

            const history = portal.getTravelHistory('Unknown Person');
            expect(history).toBe('');
        });
    });

    describe('Portal Properties', () => {
        test('should expose portal ID', () => {
            expect(portal.portalId).toBeDefined();
        });

        test('should expose status', () => {
            expect(portal.status).toBeDefined();
        });
    });
});

describe('Traveler', () => {
    let traveler: Traveler;

    beforeEach(() => {
        traveler = new Traveler('Dr. Elena Vance', 'VISA-2024-001');
    });

    describe('Initial State', () => {
        test('should have correct name', () => {
            expect(traveler.name).toBe('Dr. Elena Vance');
        });

        test('should have correct visa code', () => {
            expect(traveler.visaCode).toBe('VISA-2024-001');
        });

        test('should start with zero jumps', () => {
            expect(traveler.jumpCount).toBe(0);
        });

        test('should start with STABLE health', () => {
            expect(traveler.getHealthStatus()).toBe('STABLE');
        });

        test('should not have temporal sickness initially', () => {
            expect(traveler.hasTemporalSickness()).toBe(false);
        });
    });

    describe('Year Tracking', () => {
        test('should track current year', () => {
            traveler.setCurrentYear(1985);
            expect(traveler.currentYear).toBe(1985);
        });
    });

    describe('Jump Count and Health', () => {
        test('should increment jump count', () => {
            traveler.incrementJumpCount();
            expect(traveler.jumpCount).toBe(1);
        });

        test('should degrade health after many jumps', () => {
            for (let i = 0; i < 6; i++) {
                traveler.incrementJumpCount();
            }
            expect(traveler.getHealthStatus()).not.toBe('STABLE');
        });

        test('should become critical after excessive jumps', () => {
            for (let i = 0; i < 11; i++) {
                traveler.incrementJumpCount();
            }
            expect(traveler.getHealthStatus()).toBe('CRITICAL');
        });
    });

    describe('Temporal Sickness', () => {
        test('should have temporal sickness after excessive jumps', () => {
            for (let i = 0; i < 11; i++) {
                traveler.incrementJumpCount();
            }
            expect(traveler.hasTemporalSickness()).toBe(true);
        });

        test('should have temporal sickness immediately after a jump', () => {
            traveler.incrementJumpCount();
            expect(traveler.hasTemporalSickness()).toBe(true);
        });
    });
});

describe('PortalNetwork', () => {
    let network: PortalNetwork;

    beforeEach(() => {
        network = new PortalNetwork('Global Temporal Network');
    });

    describe('Initial State', () => {
        test('should have correct network name', () => {
            expect(network.networkName).toBe('Global Temporal Network');
        });

        test('should start with no portals', () => {
            expect(network.portals).toHaveLength(0);
        });

        test('should have zero total energy initially', () => {
            expect(network.getTotalEnergy()).toBe(0);
        });
    });

    describe('Portal Management', () => {
        test('should add portals to the network', () => {
            network.addPortal(new TimePortal('PORTAL-001'));
            network.addPortal(new TimePortal('PORTAL-002'));

            expect(network.portals).toHaveLength(2);
        });
    });

    describe('Total Energy', () => {
        test('should sum energy from all portals', () => {
            const portal1 = new TimePortal('PORTAL-001');
            portal1.charge(100);

            const portal2 = new TimePortal('PORTAL-002');
            portal2.charge(150);

            network.addPortal(portal1);
            network.addPortal(portal2);

            expect(network.getTotalEnergy()).toBe(250);
        });
    });

    describe('Finding Active Portals', () => {
        test('should return null when no suitable portal exists', () => {
            expect(network.findActivePortalWithEnergy(100)).toBeNull();
        });

        test('should find active portal with sufficient energy', () => {
            const portal = new TimePortal('PORTAL-001');
            portal.activate();
            portal.charge(200);
            network.addPortal(portal);

            expect(network.findActivePortalWithEnergy(100)).not.toBeNull();
        });

        test('should not find inactive portals', () => {
            const inactivePortal = new TimePortal('INACTIVE');
            inactivePortal.charge(500);
            network.addPortal(inactivePortal);

            expect(network.findActivePortalWithEnergy(100)).toBeNull();
        });

        test('should not find portals with insufficient energy', () => {
            const lowEnergyPortal = new TimePortal('LOW-ENERGY');
            lowEnergyPortal.activate();
            lowEnergyPortal.charge(50);
            network.addPortal(lowEnergyPortal);

            expect(network.findActivePortalWithEnergy(100)).toBeNull();
        });
    });
});
