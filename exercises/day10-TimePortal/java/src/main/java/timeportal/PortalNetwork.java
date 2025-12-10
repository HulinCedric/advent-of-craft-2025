package timeportal;

import java.util.ArrayList;
import java.util.List;

public class PortalNetwork {
    private List<TimePortal> portals;
    private String networkName;

    public PortalNetwork(String networkName) {
        this.networkName = networkName;
        this.portals = new ArrayList<>();
    }

    public void addPortal(TimePortal portal) {
        portals.add(portal);
    }

    public int getTotalEnergy() {
        int total = 0;
        for (int i = 0; i < portals.size(); i++) {
            total = total + portals.get(i).getEnergyLevel();
        }
        return total;
    }

    public TimePortal findActivePortalWithEnergy(int minimumEnergy) {
        for (int i = 0; i < portals.size(); i++) {
            if (portals.get(i).isActive() && portals.get(i).getEnergyLevel() >= minimumEnergy) {
                return portals.get(i);
            }
        }
        return null;
    }

    public String getNetworkName() {
        return networkName;
    }

    public List<TimePortal> getPortals() {
        return portals;
    }
}
