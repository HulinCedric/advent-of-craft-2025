import ci.dependencies.Logger

import scala.collection.mutable.ListBuffer


class CapturingLogger extends Logger {
  private var lines: ListBuffer[String] = ListBuffer()

  def getLoggedLines: Seq[String] = lines.toList

  override def info(message: String): Unit = lines += "INFO: " + message

  override def error(message: String): Unit = lines += "ERROR: " + message

  def startCapture(): Unit = lines = ListBuffer()
}