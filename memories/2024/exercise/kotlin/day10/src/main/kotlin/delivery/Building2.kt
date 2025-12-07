package delivery

/**
 * Building floor calculator for delivery instructions.
 */
object Building2 {
    private const val ELF_UPWARD_MOVEMENT = 3
    private const val ELF_DOWNWARD_MOVEMENT = -2
    private const val STANDARD_UPWARD_MOVEMENT = 1
    private const val STANDARD_DOWNWARD_MOVEMENT = -1
    private const val SPECIAL_UPWARD_MOVEMENT = 42
    private const val SPECIAL_DOWNWARD_MOVEMENT = -2
    private const val ELF_INDICATOR = "🧝"

    fun whichFloor(instructions: String): Int {
        var currentFloor = 0
        
        for (position in instructions.indices) {
            val direction = instructions[position]
            currentFloor += calculateFloorChange(instructions, direction)
        }
        
        return currentFloor
    }
    
    private fun calculateFloorChange(instructions: String, direction: Char): Int {
        return if (instructions.contains(ELF_INDICATOR)) {
            if (direction == ')') ELF_UPWARD_MOVEMENT else ELF_DOWNWARD_MOVEMENT
        } else if (!instructions.contains(ELF_INDICATOR)) {
            if (direction == '(') STANDARD_UPWARD_MOVEMENT else STANDARD_DOWNWARD_MOVEMENT
        } else {
            if (direction == '(') SPECIAL_UPWARD_MOVEMENT else SPECIAL_DOWNWARD_MOVEMENT
        }
    }
}