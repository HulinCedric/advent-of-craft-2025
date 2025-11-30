package ci.dependencies

trait Emailer {
  def send(message: String): Unit
}
