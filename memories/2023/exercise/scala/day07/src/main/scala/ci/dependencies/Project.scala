package ci.dependencies

import ci.dependencies.TestStatus.TestStatus

class Project private (val buildsSuccessfully: Boolean, val testStatus: TestStatus) {

  def hasTests: Boolean = testStatus != TestStatus.NO_TESTS

  def runTests: String = if (testStatus == TestStatus.PASSING_TESTS) "success" else "failure"

  def deploy: String = if (buildsSuccessfully) "success" else "failure"
}

object Project {

  def builder(): ProjectBuilder = new ProjectBuilder

  class ProjectBuilder {
    private var buildsSuccessfully: Boolean = _
    private var testStatus: TestStatus = _

    def setTestStatus(testStatus: TestStatus): this.type = {
      this.testStatus = testStatus
      this
    }

    def setDeploysSuccessfully(buildsSuccessfully: Boolean): this.type = {
      this.buildsSuccessfully = buildsSuccessfully
      this
    }

    def build(): Project = new Project(buildsSuccessfully, testStatus)
  }
}
