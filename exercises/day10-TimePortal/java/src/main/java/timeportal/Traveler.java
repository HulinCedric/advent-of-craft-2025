package timeportal;

public class Traveler {
    private String name;
    private String visaCode;
    private int currentYear;
    private int jumpCount;
    private String healthStatus; // "STABLE", "UNSTABLE", "CRITICAL"
    private long lastJumpTimestamp;

    public Traveler(String name, String visaCode) {
        this.name = name;
        this.visaCode = visaCode;
        this.currentYear = 2024;
        this.jumpCount = 0;
        this.healthStatus = "STABLE";
        this.lastJumpTimestamp = 0;
    }

    public boolean hasTemporalSickness() {
        if (jumpCount > 10) {
            return true;
        } else {
            if (System.currentTimeMillis() - lastJumpTimestamp < 3600000) {
                return true;
            } else {
                return false;
            }
        }
    }

    public void incrementJumpCount() {
        jumpCount++;
        lastJumpTimestamp = System.currentTimeMillis();
        if (jumpCount > 5) {
            healthStatus = "UNSTABLE";
        }
        if (jumpCount > 10) {
            healthStatus = "CRITICAL";
        }
    }

    public String getName() {
        return name;
    }

    public String getVisaCode() {
        return visaCode;
    }

    public int getCurrentYear() {
        return currentYear;
    }

    public void setCurrentYear(int year) {
        this.currentYear = year;
    }

    public String getHealthStatus() {
        return healthStatus;
    }

    public int getJumpCount() {
        return jumpCount;
    }
}
