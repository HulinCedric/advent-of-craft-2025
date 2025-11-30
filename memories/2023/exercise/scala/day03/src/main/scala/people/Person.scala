package people

import people.PetType.PetType


case class Person(firstName: String, lastName: String, pets: List[Pet] = Nil) {
  def addPet(petType: PetType, name: String, age: Int): Person =
    copy(pets = Pet(petType, name, age) :: pets)
}