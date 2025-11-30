package games

object FizzBuzz {
  private val MIN = 0
  private val MAX = 100
  private val FIZZ = 3
  private val BUZZ = 5

  def convert(input: Integer): String =
    if (isOutOfRange(input)) throw new OutOfRangeException
    convertSafely(input)

  private def convertSafely(input: Integer): String =
    if (is(FIZZ, input) && is(BUZZ, input)) {
      return "FizzBuzz"
    }
    if (is(FIZZ, input)) {
      return "Fizz"
    }
    if(is(BUZZ, input)) {
      return "Buzz"
    }
    input.toString


  private def is(divisor: Int, input: Int): Boolean = input % divisor == 0

  private def isOutOfRange(input: Int) = input <= MIN || input > MAX
}