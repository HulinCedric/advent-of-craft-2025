import { TimePortal } from './TimePortal';

export class PortalNetwork {
    private _networkName: string;
    private _portals: TimePortal[];

    constructor(networkName: string) {
        this._networkName = networkName;
        this._portals = [];
    }

    addPortal(portal: TimePortal): void {
        this._portals.push(portal);
    }

    getTotalEnergy(): number {
        let total = 0;
        for (let i = 0; i < this._portals.length; i++) {
            total = total + this._portals[i].energyLevel;
        }
        return total;
    }

    findActivePortalWithEnergy(minimumEnergy: number): TimePortal | null {
        for (let i = 0; i < this._portals.length; i++) {
            if (this._portals[i].isActive && this._portals[i].energyLevel >= minimumEnergy) {
                return this._portals[i];
            }
        }
        return null;
    }

    get networkName(): string {
        return this._networkName;
    }

    get portals(): TimePortal[] {
        return this._portals;
    }
}
