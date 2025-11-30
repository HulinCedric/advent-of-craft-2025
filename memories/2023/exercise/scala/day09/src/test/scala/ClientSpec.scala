import account.Client
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class ClientSpec extends AnyFlatSpec with Matchers {

  private val client = new Client(Map(
    "Tenet Deluxe Edition" -> 45.99,
    "Inception" -> 30.50,
    "The Dark Knight" -> 30.50,
    "Interstellar" -> 23.98
  ))

  "Client" should "return correct statement and total amount" in {
    val statement = client.toStatement()

    client.getTotalAmount() shouldBe 130.97
    statement shouldBe
      """Tenet Deluxe Edition for 45.99€
        |Inception for 30.5€
        |The Dark Knight for 30.5€
        |Interstellar for 23.98€
        |Total : 130.97€""".stripMargin
  }
}
