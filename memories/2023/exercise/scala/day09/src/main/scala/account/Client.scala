package account

class Client(private val orderLines: Map[String, Double]) {
  private var totalAmount: Double = 0.0

  def toStatement(): String = {
    orderLines.map { case (key, value) => formatLine(key, value) }
      .mkString(System.lineSeparator())
      .concat(System.lineSeparator() + s"Total : $totalAmount€")
  }

  private def formatLine(name: String, value: Double): String = {
    totalAmount += value
    s"$name for $value€"
  }

  def getTotalAmount(): Double = totalAmount
}
