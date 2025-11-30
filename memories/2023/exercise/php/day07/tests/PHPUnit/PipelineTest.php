<?php

declare(strict_types = 1);

use App\Dependancies\Config;
use App\Dependancies\Emailer;
use App\Dependancies\Project;
use App\Dependancies\TestStatus;
use App\Pipeline;
use PHPUnit\Framework\TestCase;
use Tests\CapturingLogger;

class PipelineTest extends TestCase
{
	private $config;

	private $logger;

	private $emailer;

	private $pipeline;

	protected function setUp(): void
	{
		$this->config = $this->createMock(Config::class);
		$this->logger = new CapturingLogger;
		$this->emailer = $this->createMock(Emailer::class);

		$this->pipeline = new Pipeline($this->config, $this->emailer, $this->logger);
	}

	public function test_project_with_tests_that_deploys_successfully_with_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(true);

		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment completed successfully'));

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: Tests passed',
			'INFO: Deployment successful',
			'INFO: Sending email',
		], $this->logger->getLoggedLines());
	}

	public function test_project_with_tests_that_deploys_successfully_without_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(false);

		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: Tests passed',
			'INFO: Deployment successful',
			'INFO: Email disabled',
		], $this->logger->getLoggedLines());
	}

	public function test_project_without_tests_that_deploys_successfully_with_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(true);

		$project = (Project::builder())->
			setTestStatus(TestStatus::NoTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment completed successfully'));

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: No tests',
			'INFO: Deployment successful',
			'INFO: Sending email',
		], $this->logger->getLoggedLines());
	}

	public function test_project_without_tests_that_deploys_successfully_without_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(false);

		$project = (Project::builder())->
			setTestStatus(TestStatus::NoTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: No tests',
			'INFO: Deployment successful',
			'INFO: Email disabled',
		], $this->logger->getLoggedLines());
	}

	public function test_project_with_tests_that_fail_with_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(true);

		$project = (Project::builder())->
			setTestStatus(TestStatus::FailingTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Tests failed'));

		$this->pipeline->run($project);

		$this->assertSame([
			'ERROR: Tests failed',
			'INFO: Sending email',
		], $this->logger->getLoggedLines());
	}

	public function test_project_with_tests_that_fail_without_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(false);

		$project = (Project::builder())->
			setTestStatus(TestStatus::FailingTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		$this->assertSame([
			'ERROR: Tests failed',
			'INFO: Email disabled',
		], $this->logger->getLoggedLines());
	}

	public function test_project_with_tests_and_failing_build_with_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(true);

		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment failed'));

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: Tests passed',
			'ERROR: Deployment failed',
			'INFO: Sending email',
		], $this->logger->getLoggedLines());
	}

	public function test_project_with_tests_and_failing_build_without_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(false);

		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: Tests passed',
			'ERROR: Deployment failed',
			'INFO: Email disabled',
		], $this->logger->getLoggedLines());
	}

	public function test_project_without_tests_and_failing_build_with_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(true);

		$project = (Project::builder())->
			setTestStatus(TestStatus::NoTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment failed'));

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: No tests',
			'ERROR: Deployment failed',
			'INFO: Sending email',
		], $this->logger->getLoggedLines());
	}

	public function test_project_without_tests_and_failing_build_without_email_notification(): void
	{
		$this->config->method('sendEmailSummary')->willReturn(false);

		$project = (Project::builder())->
			setTestStatus(TestStatus::NoTests)->
			setDeploysSuccessfully(false)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		$this->assertSame([
			'INFO: No tests',
			'ERROR: Deployment failed',
			'INFO: Email disabled',
		], $this->logger->getLoggedLines());
	}
}
