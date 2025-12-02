import games.FizzBuzz
import io.kotest.assertions.arrow.core.shouldBeNone
import io.kotest.assertions.arrow.core.shouldBeSome
import io.kotest.core.spec.style.FunSpec
import io.kotest.datatest.withData

private const val MIN = 1
private const val MAX = 2000

class FizzBuzzTests : FunSpec({
    // inject mapping without the composite 15 -> "FizzBuzz" so outputs are accumulated
    val fizzBuzz = FizzBuzz(listOf(3 to "Fizz", 5 to "Buzz", 7 to "Whizz", 11 to "Bang"), MIN, MAX)

    context("returns its numbers representation") {
        withData(
            // Number-only
            ValidInput(1, "1"),
            ValidInput(67, "67"),
            ValidInput(82, "82"),
            // Fizz-only
            ValidInput(3, "Fizz"),
            ValidInput(6, "Fizz"),
            ValidInput(9, "Fizz"),
            // Buzz-only
            ValidInput(5, "Buzz"),
            ValidInput(50, "Buzz"),
            ValidInput(85, "Buzz"),
            // Fizz + Buzz
            ValidInput(15, "FizzBuzz"),
            ValidInput(30, "FizzBuzz"),
            ValidInput(45, "FizzBuzz"),
            // Whizz-only
            ValidInput(7, "Whizz"),
            ValidInput(14, "Whizz"),
            ValidInput(28, "Whizz"),
            // Fizz + Whizz
            ValidInput(21, "FizzWhizz"),
            ValidInput(42, "FizzWhizz"),
            ValidInput(63, "FizzWhizz"),
            // Buzz + Whizz
            ValidInput(35, "BuzzWhizz"),
            ValidInput(70, "BuzzWhizz"),
            // Bang-only
            ValidInput(11, "Bang"),
            ValidInput(22, "Bang"),
            ValidInput(44, "Bang"),
            // Fizz + Bang
            ValidInput(33, "FizzBang"),
            ValidInput(66, "FizzBang"),
            ValidInput(99, "FizzBang"),
            // Buzz + Bang
            ValidInput(55, "BuzzBang"),
            // Whizz + Bang
            ValidInput(77, "WhizzBang"),

            // Additional cases beyond 100 up to MAX
            ValidInput(110, "BuzzBang"),
            ValidInput(140, "BuzzWhizz"),
            ValidInput(154, "WhizzBang"),
            ValidInput(165, "FizzBuzzBang"),
            ValidInput(210, "FizzBuzzWhizz"),
            ValidInput(231, "FizzWhizzBang"),
            ValidInput(385, "BuzzWhizzBang"),
            ValidInput(420, "FizzBuzzWhizz"),
            ValidInput(105, "FizzBuzzWhizz"),
            ValidInput(1155, "FizzBuzzWhizzBang")

        ) { (input, expectedResult) ->
            fizzBuzz.convert(input).shouldBeSome(expectedResult)
        }
    }

    context("fails for numbers out of range") {
        withData(0, -1, 2001) { x ->
            fizzBuzz.convert(x).shouldBeNone()
        }
    }
})

data class ValidInput(val input: Int, val expectedResult: String)