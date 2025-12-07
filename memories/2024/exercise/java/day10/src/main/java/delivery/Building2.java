package delivery;

public class Building2 {
    
    private static final int ELF_UPWARD_MOVEMENT = 3;
    private static final int ELF_DOWNWARD_MOVEMENT = -2;
    private static final int STANDARD_UPWARD_MOVEMENT = 1;
    private static final int STANDARD_DOWNWARD_MOVEMENT = -1;
    private static final int SPECIAL_UPWARD_MOVEMENT = 42;
    private static final int SPECIAL_DOWNWARD_MOVEMENT = -2;
    private static final String ELF_INDICATOR = "🧝";

    public static int whichFloor(String instructions) {
        int currentFloor = 0;
        
        for (int position = 0; position < instructions.length(); position++) {
            char direction = instructions.charAt(position);
            currentFloor += calculateFloorChange(instructions, direction);
        }
        
        return currentFloor;
    }

    private static int calculateFloorChange(String instructions, char direction) {
        if (instructions.contains(ELF_INDICATOR)) {
            return direction == ')' 
                ? ELF_UPWARD_MOVEMENT 
                : ELF_DOWNWARD_MOVEMENT;
        } else if (!instructions.contains(ELF_INDICATOR)) {
            return direction == '(' 
                ? STANDARD_UPWARD_MOVEMENT 
                : STANDARD_DOWNWARD_MOVEMENT;
        } else {
            return direction == '(' 
                ? SPECIAL_UPWARD_MOVEMENT 
                : SPECIAL_DOWNWARD_MOVEMENT;
        }
    }
}
