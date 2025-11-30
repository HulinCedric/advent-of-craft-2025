package greeting

class Greeter {
  private var formality: String = _

  def greet(): String = formality match {
    case null => "Hello."
    case "formal" => "Good evening, sir."
    case "casual" => "Sup bro?"
    case "intimate" => "Hello Darling!"
    case _ => "Hello."
  }

  def setFormality(formality: String): Unit = {
    this.formality = formality
  }
}
