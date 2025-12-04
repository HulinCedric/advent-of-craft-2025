package games

import arrow.core.None
import arrow.core.Option
import arrow.core.Some

const val MIN = 1
const val MAX = 100
private const val FIZZBUZZ = 15
private const val FIZZ = 3
private const val BUZZ = 5
private const val WHIZZ = 7
private const val BANG = 11
private const val FIZZBANG = 21
private const val BUZZBANG = 35
private const val FIZZBUZZWHIZ = 75
private const val WHIZBANG = 77

object FizzBuzz {
    fun convert(input: Int): Option<String> = when {
        isOutOfRange(input) -> None
        else -> Some(convertSafely(input))
    }

    private fun convertSafely(input: Int): String = when {
        `is`(WHIZBANG, input) -> "WhizBang"
        `is`(FIZZBUZZWHIZ, input) -> "FizzBuzzWhiz"
        `is`(BUZZBANG, input) -> "BuzzBang"
        `is`(FIZZBANG, input) -> "FizzBang"
        `is`(FIZZBUZZ, input) -> "FizzBuzz"
        `is`(FIZZ, input) -> "Fizz"
        `is`(BUZZ, input) -> "Buzz"
        `is`(WHIZZ, input) -> "Whizz"
        `is`(BANG, input) -> "Bang"
        else -> input.toString()
    }

    private fun `is`(divisor: Int, input: Int): Boolean = input % divisor == 0
    private fun isOutOfRange(input: Int) = input < MIN || input > MAX
}