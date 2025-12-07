package delivery

/**
 * Building floor calculator for delivery instructions.
 */
object Building4 {
    private const val ELF_UP = 3
    private const val ELF_DOWN = -2
    private const val STANDARD_UP = 1
    private const val STANDARD_DOWN = -1
    private const val ELF_INDICATOR = "🧝"

    fun whichFloor(instructions: String): Int {
        val isElfMode = instructions.contains(ELF_INDICATOR)
        
        return instructions
            .filter { it == '(' || it == ')' }
            .sumOf { calculateFloorMovement(it, isElfMode) }
    }
    
    private fun calculateFloorMovement(direction: Char, isElfMode: Boolean): Int {
        return if (isElfMode) {
            if (direction == ')') ELF_UP else ELF_DOWN
        } else {
            if (direction == '(') STANDARD_UP else STANDARD_DOWN
        }
    }
}