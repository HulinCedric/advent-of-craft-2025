package greeting

import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class GreeterSpec extends AnyFlatSpec with Matchers {

  "Greeter" should "say Hello" in {
    val greeter = new Greeter()
    greeter.greet() shouldEqual "Hello."
  }

  it should "say Good evening, sir. formally" in {
    val greeter = new Greeter()
    greeter.setFormality("formal")
    greeter.greet() shouldEqual "Good evening, sir."
  }

  it should "say Sup bro? casually" in {
    val greeter = new Greeter()
    greeter.setFormality("casual")
    greeter.greet() shouldEqual "Sup bro?"
  }

  it should "say Hello Darling! intimately" in {
    val greeter = new Greeter()
    greeter.setFormality("intimate")
    greeter.greet() shouldEqual "Hello Darling!"
  }
}
