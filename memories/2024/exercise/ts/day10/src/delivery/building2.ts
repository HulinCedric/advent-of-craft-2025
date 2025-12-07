/**
 * Building floor calculator for delivery instructions.
 */
export class Building2 {
    private static readonly ELF_UPWARD_MOVEMENT = 3;
    private static readonly ELF_DOWNWARD_MOVEMENT = -2;
    private static readonly STANDARD_UPWARD_MOVEMENT = 1;
    private static readonly STANDARD_DOWNWARD_MOVEMENT = -1;
    private static readonly SPECIAL_UPWARD_MOVEMENT = 42;
    private static readonly SPECIAL_DOWNWARD_MOVEMENT = -2;
    private static readonly ELF_INDICATOR = "🧝";

    static whichFloor(instructions: string): number {
        let currentFloor = 0;
        
        for (let position = 0; position < instructions.length; position++) {
            const direction = instructions[position];
            currentFloor += this.calculateFloorChange(instructions, direction);
        }
        
        return currentFloor;
    }
    
    private static calculateFloorChange(instructions: string, direction: string): number {
        if (instructions.includes(this.ELF_INDICATOR)) {
            return direction === ')' ? this.ELF_UPWARD_MOVEMENT : this.ELF_DOWNWARD_MOVEMENT;
        } else if (!instructions.includes(this.ELF_INDICATOR)) {
            return direction === '(' ? this.STANDARD_UPWARD_MOVEMENT : this.STANDARD_DOWNWARD_MOVEMENT;
        } else {
            return direction === '(' ? this.SPECIAL_UPWARD_MOVEMENT : this.SPECIAL_DOWNWARD_MOVEMENT;
        }
    }
}