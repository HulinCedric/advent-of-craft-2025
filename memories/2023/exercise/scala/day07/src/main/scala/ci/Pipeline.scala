package ci

import ci.dependencies.{Config, Emailer, Logger, Project}

class Pipeline(private val config: Config, private val emailer: Emailer, private val log: Logger) {

  def run(project: Project): Unit = {
    var testsPassed = false
    var deploySuccessful = false

    if (project.hasTests) {
      if ("success" == project.runTests) {
        log.info("Tests passed")
        testsPassed = true
      } else {
        log.error("Tests failed")
        testsPassed = false
      }
    } else {
      log.info("No tests")
      testsPassed = true
    }

    if (testsPassed) {
      if ("success" == project.deploy) {
        log.info("Deployment successful")
        deploySuccessful = true
      } else {
        log.error("Deployment failed")
        deploySuccessful = false
      }
    } else {
      deploySuccessful = false
    }

    if (config.sendEmailSummary()) {
      log.info("Sending email")
      if (testsPassed) {
        if (deploySuccessful) {
          emailer.send("Deployment completed successfully")
        } else {
          emailer.send("Deployment failed")
        }
      } else {
        emailer.send("Tests failed")
      }
    } else {
      log.info("Email disabled")
    }
  }
}
