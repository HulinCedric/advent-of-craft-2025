package delivery;

public class Building4 {
    
    private static final int ELF_UP = 3;
    private static final int ELF_DOWN = -2;
    private static final int STANDARD_UP = 1;
    private static final int STANDARD_DOWN = -1;
    private static final String ELF_INDICATOR = "🧝";

    public static int whichFloor(String instructions) {
        boolean isElfMode = instructions.contains(ELF_INDICATOR);
        
        return instructions.chars()
            .filter(ch -> ch == '(' || ch == ')')
            .map(ch -> calculateFloorMovement(ch, isElfMode))
            .sum();
    }

    private static int calculateFloorMovement(int direction, boolean isElfMode) {
        if (isElfMode) {
            return direction == ')' ? ELF_UP : ELF_DOWN;
        } else {
            return direction == '(' ? STANDARD_UP : STANDARD_DOWN;
        }
    }
}
