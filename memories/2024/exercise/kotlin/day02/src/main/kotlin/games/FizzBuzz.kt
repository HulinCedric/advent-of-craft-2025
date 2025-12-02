package games

import arrow.core.None
import arrow.core.Option
import arrow.core.Some

class FizzBuzz(
    private val rules: List<Pair<Int, String>>,
    private val min: Int,
    private val max: Int
) {
    fun convert(input: Int): Option<String> = when {
        isOutOfRange(input) -> None
        else -> Some(convertSafely(input))
    }

    private fun convertSafely(input: Int): String =
        rules
            .mapNotNull { (divisor, output) -> if (`is`(divisor, input)) output else null }
            .ifEmpty { listOf(input.toString()) }
            .joinToString(separator = "")

    private fun `is`(divisor: Int, input: Int): Boolean = input % divisor == 0
    private fun isOutOfRange(input: Int) = input < min || input > max
}