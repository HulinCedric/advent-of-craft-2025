package timeportal;

import java.util.ArrayList;
import java.util.List;

public class TimePortal {
    private String portalId;
    private int energyLevel;
    private List<String> travelLog;
    private boolean isActive;
    private String currentStatus; // "IDLE", "CHARGING", "JUMPING", "ERROR"

    public TimePortal(String portalId) {
        this.portalId = portalId;
        this.energyLevel = 0;
        this.travelLog = new ArrayList<>();
        this.isActive = false;
        this.currentStatus = "IDLE";
    }

    public String initiateJump(Traveler traveler, int destinationYear, String coordinates) {
        if (isActive) {
            if (energyLevel >= 100) {
                if (destinationYear >= -10000 && destinationYear <= 3000) {
                    if (coordinates != null && coordinates.length() == 8) {
                        if (traveler.getHealthStatus().equals("STABLE")) {
                            if (!traveler.hasTemporalSickness()) {
                                currentStatus = "JUMPING";
                                energyLevel = energyLevel - 100;
                                String logEntry = traveler.getName() + "|" + destinationYear + "|" + coordinates + "|" + System.currentTimeMillis();
                                travelLog.add(logEntry);
                                traveler.setCurrentYear(destinationYear);
                                traveler.incrementJumpCount();
                                currentStatus = "IDLE";
                                return "SUCCESS: " + traveler.getName() + " transported to year " + destinationYear;
                            } else {
                                return "ERROR: Traveler has temporal sickness";
                            }
                        } else {
                            return "ERROR: Traveler health unstable";
                        }
                    } else {
                        return "ERROR: Invalid coordinates format";
                    }
                } else {
                    return "ERROR: Destination year out of range";
                }
            } else {
                return "ERROR: Insufficient energy. Current: " + energyLevel;
            }
        } else {
            return "ERROR: Portal is not active";
        }
    }

    public void charge(int amount) {
        if (amount > 0) {
            if (currentStatus.equals("IDLE") || currentStatus.equals("CHARGING")) {
                currentStatus = "CHARGING";
                energyLevel = energyLevel + amount;
                if (energyLevel > 500) {
                    energyLevel = 500;
                }
                currentStatus = "IDLE";
            }
        }
    }

    public String getTravelHistory(String travelerName) {
        String result = "";
        for (int i = 0; i < travelLog.size(); i++) {
            String[] parts = travelLog.get(i).split("\\|");
            if (parts[0].equals(travelerName)) {
                result = result + "Year: " + parts[1] + ", Coords: " + parts[2] + "\n";
            }
        }
        return result;
    }

    public String getPortalId() {
        return portalId;
    }

    public int getEnergyLevel() {
        return energyLevel;
    }

    public boolean isActive() {
        return isActive;
    }

    public void activate() {
        this.isActive = true;
    }

    public void deactivate() {
        this.isActive = false;
    }

    public String getStatus() {
        return currentStatus;
    }

    public List<String> getTravelLog() {
        return travelLog;
    }
}
