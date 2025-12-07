package delivery

object Building3 {
    fun whichFloor(instructions: String): Int {
        val hasElf = instructions.contains("🧝")
        val valList = mutableListOf<Int>()
        
        for (i in instructions.indices) {
            val c = instructions[i]
            
            if (hasElf) {
                valList.add(if (c == ')') 3 else -2)
            } else {
                valList.add(if (c == '(') 1 else -1)
            }
        }
        
        var result = 0
        for (v in valList) {
            result += v
        }
        return result
    }
}