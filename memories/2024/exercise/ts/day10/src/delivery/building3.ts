export class Building3 {
    static whichFloor(instructions: string): number {
        const hasElf = instructions.includes("🧝");
        let val: number[] = [];
        
        for (let i = 0; i < instructions.length; i++) {
            const c = instructions[i];
            
            if (hasElf) {
                val.push(c === ')' ? 3 : -2);
            } else {
                val.push(c === '(' ? 1 : -1);
            }
        }
        
        let result = 0;
        for (const v of val) {
            result += v;
        }
        return result;
    }
}