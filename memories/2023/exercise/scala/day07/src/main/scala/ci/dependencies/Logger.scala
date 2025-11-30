package ci.dependencies

trait Logger {
  def info(message: String): Unit

  def error(message: String): Unit
}
