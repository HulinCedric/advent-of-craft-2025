package ci.dependencies

object TestStatus extends Enumeration {
  type TestStatus = Value
  val NO_TESTS, PASSING_TESTS, FAILING_TESTS = Value
}
