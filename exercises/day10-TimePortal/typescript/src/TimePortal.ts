import { Traveler } from './Traveler';

export class TimePortal {
    private _portalId: string;
    private _energyLevel: number;
    private _travelLog: string[];
    private _isActive: boolean;
    private _currentStatus: string; // "IDLE", "CHARGING", "JUMPING", "ERROR"

    constructor(portalId: string) {
        this._portalId = portalId;
        this._energyLevel = 0;
        this._travelLog = [];
        this._isActive = false;
        this._currentStatus = "IDLE";
    }

    initiateJump(traveler: Traveler, destinationYear: number, coordinates: string | null): string {
        if (this._isActive) {
            if (this._energyLevel >= 100) {
                if (destinationYear >= -10000 && destinationYear <= 3000) {
                    if (coordinates !== null && coordinates.length === 8) {
                        if (traveler.getHealthStatus() === "STABLE") {
                            if (!traveler.hasTemporalSickness()) {
                                this._currentStatus = "JUMPING";
                                this._energyLevel = this._energyLevel - 100;
                                const logEntry = `${traveler.name}|${destinationYear}|${coordinates}|${Date.now()}`;
                                this._travelLog.push(logEntry);
                                traveler.setCurrentYear(destinationYear);
                                traveler.incrementJumpCount();
                                this._currentStatus = "IDLE";
                                return `SUCCESS: ${traveler.name} transported to year ${destinationYear}`;
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
                return `ERROR: Insufficient energy. Current: ${this._energyLevel}`;
            }
        } else {
            return "ERROR: Portal is not active";
        }
    }

    charge(amount: number): void {
        if (amount > 0) {
            if (this._currentStatus === "IDLE" || this._currentStatus === "CHARGING") {
                this._currentStatus = "CHARGING";
                this._energyLevel = this._energyLevel + amount;
                if (this._energyLevel > 500) {
                    this._energyLevel = 500;
                }
                this._currentStatus = "IDLE";
            }
        }
    }

    getTravelHistory(travelerName: string): string {
        let result = "";
        for (let i = 0; i < this._travelLog.length; i++) {
            const parts = this._travelLog[i].split("|");
            if (parts[0] === travelerName) {
                result = result + `Year: ${parts[1]}, Coords: ${parts[2]}\n`;
            }
        }
        return result;
    }

    get portalId(): string {
        return this._portalId;
    }

    get energyLevel(): number {
        return this._energyLevel;
    }

    get isActive(): boolean {
        return this._isActive;
    }

    activate(): void {
        this._isActive = true;
    }

    deactivate(): void {
        this._isActive = false;
    }

    get status(): string {
        return this._currentStatus;
    }

    get travelLog(): string[] {
        return this._travelLog;
    }
}
