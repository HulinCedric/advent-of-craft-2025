import org.scalatest.BeforeAndAfterEach
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers
import people.{Person, PetType}

class PopulationSpec extends AnyFlatSpec with Matchers with BeforeAndAfterEach {

  private var population: List[Person] = _

  override def beforeEach(): Unit = {
    population = List(
      Person("Stewie", "Griffin")
        .addPet(PetType.CAT, "Dolly", 3)
        .addPet(PetType.DOG, "Brian", 9),
      Person("Joe", "Swanson")
        .addPet(PetType.DOG, "Spike", 4),
      Person("Lois", "Griffin")
        .addPet(PetType.SNAKE, "Serpy", 1),
      Person("Meg", "Griffin")
        .addPet(PetType.BIRD, "Tweety", 1),
      Person("Chris", "Griffin")
        .addPet(PetType.TURTLE, "Speedy", 4),
      Person("Cleveland", "Brown")
        .addPet(PetType.HAMSTER, "Fuzzy", 1)
        .addPet(PetType.HAMSTER, "Wuzzy", 2),
      Person("Glenn", "Quagmire")
    )
  }

  "PopulationTests" should "identify the owner of the youngest pet" in {
    val filtered = population.minBy(person =>
      person.pets.map(_.age).minOption.getOrElse(Integer.MAX_VALUE)
    )

    assert(filtered != null)
    filtered.firstName shouldBe "Lois"
  }
}
