package games

import arrow.core.None
import arrow.core.Option
import arrow.core.Some

class FizzBuzz(
    private val rules: Map<Int, String>,
    private val min: Int,
    private val max: Int
) {
    fun convert(input: Int): Option<String> = when {
        isOutOfRange(input) -> None
        else -> Some(convertSafely(input))
    }

    private fun convertSafely(input: Int): String =
        rules.entries
            .mapNotNull { (divisor, output) -> if (input % divisor == 0) output else null }
            .ifEmpty { listOf(input.toString()) }
            .joinToString(separator = "")

    private fun isOutOfRange(input: Int) = input !in min..max
}