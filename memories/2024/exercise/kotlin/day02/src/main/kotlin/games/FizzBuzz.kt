package games

import arrow.core.None
import arrow.core.Option
import arrow.core.Some
typealias Rules = Map<Int, String>
typealias MatchingRule = Collection<String>

class FizzBuzz(
    private val rules: Rules,
    private val min: Int,
    private val max: Int
) {
    fun convert(input: Int): Option<String> = when {
        isOutOfRange(input) -> None
        rules.isEmpty() -> None
        else -> Some(convertSafely(input))
    }

    private fun isOutOfRange(input: Int) = input !in min..max

    private fun convertSafely(input: Int): String =
        rules.match(input)
            .values
            .toResult(input)

    private fun Rules.match(input: Int): Map<Int, String> = filter { (divisor, _) -> `is`(divisor, input) }

    private fun `is`(divisor: Int, input: Int): Boolean = input % divisor == 0

    private fun MatchingRule.toResult(input: Int): String = when {
        any() -> joinToString("")
        else -> input.toString()
    }
}