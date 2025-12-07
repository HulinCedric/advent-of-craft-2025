/**
 * Building floor calculator for delivery instructions.
 */
export class Building4 {
    private static readonly ELF_UP = 3;
    private static readonly ELF_DOWN = -2;
    private static readonly STANDARD_UP = 1;
    private static readonly STANDARD_DOWN = -1;
    private static readonly ELF_INDICATOR = "🧝";

    static whichFloor(instructions: string): number {
        const isElfMode = instructions.includes(this.ELF_INDICATOR);
        
        return instructions
            .split('')
            .filter(ch => ch === '(' || ch === ')')
            .reduce((floor, ch) => floor + this.calculateFloorMovement(ch, isElfMode), 0);
    }
    
    private static calculateFloorMovement(direction: string, isElfMode: boolean): number {
        if (isElfMode) {
            return direction === ')' ? this.ELF_UP : this.ELF_DOWN;
        } else {
            return direction === '(' ? this.STANDARD_UP : this.STANDARD_DOWN;
        }
    }
}