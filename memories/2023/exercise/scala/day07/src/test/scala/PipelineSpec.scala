import ci.Pipeline
import ci.dependencies.{Config, Emailer, Project, TestStatus}
import eu.monniot.scala3mock.scalatest.MockFactory
import org.scalatest.BeforeAndAfterEach
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class PipelineSpec extends AnyFlatSpec with Matchers with BeforeAndAfterEach with MockFactory {

  private val config: Config = mock[Config]
  private val emailer: Emailer = mock[Emailer]
  private val log: CapturingLogger = new CapturingLogger

  private var pipeline: Pipeline = _

  override def beforeEach(): Unit = {
    pipeline = new Pipeline(config, emailer, log)
    log.startCapture()
  }

  "Pipeline" should "run project with tests that deploys successfully with email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(true)
    when(emailer.send(_: String)).expects("Deployment completed successfully").once

    val project = Project.builder()
      .setTestStatus(TestStatus.PASSING_TESTS)
      .setDeploysSuccessfully(true)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: Tests passed",
      "INFO: Deployment successful",
      "INFO: Sending email"
    )
  }

  it should "run project with tests that deploys successfully without email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(false)

    val project = Project.builder()
      .setTestStatus(TestStatus.PASSING_TESTS)
      .setDeploysSuccessfully(true)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: Tests passed",
      "INFO: Deployment successful",
      "INFO: Email disabled"
    )

    when(emailer.send(_: String)).expects(*).never
  }

  it should "run project without tests that deploys successfully with email notification" in {
    when(emailer.send(_: String)).expects("Deployment completed successfully").once
    when(() => config.sendEmailSummary()).expects().returning(true)

    val project = Project.builder()
      .setTestStatus(TestStatus.NO_TESTS)
      .setDeploysSuccessfully(true)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: No tests",
      "INFO: Deployment successful",
      "INFO: Sending email"
    )
  }

  it should "run project without tests that deploys successfully without email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(false)

    val project = Project.builder()
      .setTestStatus(TestStatus.NO_TESTS)
      .setDeploysSuccessfully(true)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: No tests",
      "INFO: Deployment successful",
      "INFO: Email disabled"
    )

    when(emailer.send(_: String)).expects(*).never
  }

  it should "run project with tests that fail with email notification" in {
    when(emailer.send(_: String)).expects("Tests failed").once
    when(() => config.sendEmailSummary()).expects().returning(true)

    val project = Project.builder()
      .setTestStatus(TestStatus.FAILING_TESTS)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "ERROR: Tests failed",
      "INFO: Sending email"
    )
  }

  it should "run project with tests that fail without email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(false)

    val project = Project.builder()
      .setTestStatus(TestStatus.FAILING_TESTS)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "ERROR: Tests failed",
      "INFO: Email disabled"
    )

    when(emailer.send(_: String)).expects(*).never
  }

  it should "run project with tests and failing build with email notification" in {
    when(emailer.send(_: String)).expects("Deployment failed").once
    when(() => config.sendEmailSummary()).expects().returning(true)

    val project = Project.builder()
      .setTestStatus(TestStatus.PASSING_TESTS)
      .setDeploysSuccessfully(false)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: Tests passed",
      "ERROR: Deployment failed",
      "INFO: Sending email"
    )
  }

  it should "run project with tests and failing build without email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(false)

    val project = Project.builder()
      .setTestStatus(TestStatus.PASSING_TESTS)
      .setDeploysSuccessfully(false)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: Tests passed",
      "ERROR: Deployment failed",
      "INFO: Email disabled"
    )

    when(emailer.send(_: String)).expects(*).never
  }

  it should "run project without tests and failing build with email notification" in {
    when(emailer.send(_: String)).expects("Deployment failed").once
    when(() => config.sendEmailSummary()).expects().returns(true)

    val project = Project.builder()
      .setTestStatus(TestStatus.NO_TESTS)
      .setDeploysSuccessfully(false)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: No tests",
      "ERROR: Deployment failed",
      "INFO: Sending email"
    )
  }

  it should "run project without tests and failing build without email notification" in {
    when(() => config.sendEmailSummary()).expects().returning(false)

    val project = Project.builder()
      .setTestStatus(TestStatus.NO_TESTS)
      .setDeploysSuccessfully(false)
      .build()

    pipeline.run(project)

    log.getLoggedLines should contain theSameElementsInOrderAs Seq(
      "INFO: No tests",
      "ERROR: Deployment failed",
      "INFO: Email disabled"
    )

    when(emailer.send(_: String)).expects(*).never
  }
}
