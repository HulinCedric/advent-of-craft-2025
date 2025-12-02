package games

import arrow.core.None
import arrow.core.Option
import arrow.core.Some

const val MIN = 1
const val MAX = 100

class FizzBuzz(private val rules: List<Pair<Int, String>>) {
    fun convert(input: Int): Option<String> = when {
        isOutOfRange(input) -> None
        else -> Some(convertSafely(input))
    }

    private fun convertSafely(input: Int): String {
        val matches = rules.filter { (divisor, _) -> `is`(divisor, input) }
        if (matches.isEmpty()) return input.toString()

        // Accumulate outputs from all matching rules in the order rules are provided
        return matches.joinToString(separator = "") { it.second }
    }

    private fun `is`(divisor: Int, input: Int): Boolean = input % divisor == 0
    private fun isOutOfRange(input: Int) = input < MIN || input > MAX
}