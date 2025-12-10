export class Traveler {
    private _name: string;
    private _visaCode: string;
    private _currentYear: number;
    private _jumpCount: number;
    private _healthStatus: string; // "STABLE", "UNSTABLE", "CRITICAL"
    private _lastJumpTimestamp: number;

    constructor(name: string, visaCode: string) {
        this._name = name;
        this._visaCode = visaCode;
        this._currentYear = 2024;
        this._jumpCount = 0;
        this._healthStatus = "STABLE";
        this._lastJumpTimestamp = 0;
    }

    hasTemporalSickness(): boolean {
        if (this._jumpCount > 10) {
            return true;
        } else {
            if (Date.now() - this._lastJumpTimestamp < 3600000) {
                return true;
            } else {
                return false;
            }
        }
    }

    incrementJumpCount(): void {
        this._jumpCount++;
        this._lastJumpTimestamp = Date.now();
        if (this._jumpCount > 5) {
            this._healthStatus = "UNSTABLE";
        }
        if (this._jumpCount > 10) {
            this._healthStatus = "CRITICAL";
        }
    }

    get name(): string {
        return this._name;
    }

    get visaCode(): string {
        return this._visaCode;
    }

    get currentYear(): number {
        return this._currentYear;
    }

    setCurrentYear(year: number): void {
        this._currentYear = year;
    }

    getHealthStatus(): string {
        return this._healthStatus;
    }

    get jumpCount(): number {
        return this._jumpCount;
    }
}
