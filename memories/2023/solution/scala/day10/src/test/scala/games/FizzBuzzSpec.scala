package games

import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers
import org.scalatest.prop.{TableDrivenPropertyChecks, TableFor1, TableFor2}

class FizzBuzzSpec extends AnyFlatSpec with Matchers with TableDrivenPropertyChecks {

  val validInputs: TableFor2[Int, String] = Table(
    ("input", "expectedResult"),
    (1, "1"),
    (67, "67"),
    (82, "82"),
    (3, "Fizz"),
    (66, "Fizz"),
    (99, "Fizz"),
    (5, "Buzz"),
    (50, "Buzz"),
    (85, "Buzz"),
    (15, "FizzBuzz"),
    (30, "FizzBuzz"),
    (45, "FizzBuzz")
  )

  val invalidInputs: TableFor1[Int] = Table("input", 0, -1, 101)

  "FizzBuzz" should "return number representation for valid inputs" in {
    forAll(validInputs) { (input, expectedResult) =>
      FizzBuzz.convert(input) shouldBe expectedResult
    }
  }

  it should "throw an exception for numbers out of range" in {
    forAll(invalidInputs) { input =>
      an[OutOfRangeException] should be thrownBy FizzBuzz.convert(input)
    }
  }
}
