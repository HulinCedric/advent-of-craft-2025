import games.FizzBuzz
import io.kotest.core.spec.style.StringSpec
import io.kotest.property.Arb
import io.kotest.property.arbitrary.filter
import io.kotest.property.arbitrary.int
import io.kotest.property.forAll

private const val MIN = 1
private const val MAX = 100

val fizzBuzzStrings = listOf(
    "Fizz", "Buzz", "Whizz", "Bang",

    "FizzBuzz", "FizzWhizz", "FizzBang",
    "BuzzWhizz", "BuzzBang",
    "WhizzBang",

    "FizzBuzzWhizz", "FizzBuzzBang",
    "FizzWhizzBang",
    "BuzzWhizzBang",

    "FizzBuzzWhizzBang")
fun validStringsFor(x: Int): List<String> = fizzBuzzStrings + x.toString()


class FizzBuzzProperties : StringSpec({
    val fizzBuzz = FizzBuzz(listOf(3 to "Fizz", 5 to "Buzz", 7 to "Whizz", 11 to "Bang"), MIN, MAX)

    "parse return a valid string for numbers between 1 and 100" {
        forAll(Arb.int(MIN..MAX)) { x ->
            fizzBuzz.convert(x).isSome { result -> validStringsFor(x).contains(result) }
        }
    }

    "parse fail for numbers out of range" {
        forAll(Arb.int().filter { i -> i < MIN || i > MAX }) { x ->
            fizzBuzz.convert(x).isNone()
        }
    }
})