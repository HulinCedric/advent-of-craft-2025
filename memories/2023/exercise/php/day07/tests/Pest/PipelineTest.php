<?php

declare(strict_types = 1);

use App\Dependancies\Config;
use App\Dependancies\Emailer;
use App\Dependancies\Project;
use App\Dependancies\TestStatus;
use App\Pipeline;
use Tests\CapturingLogger;

describe('Pipeline Tests with sendEmailSummary at true', function (): void {

	beforeEach(function (): void {
		$this->sendEmailSummary = Mockery::mock(Config::class);
		$this->sendEmailSummary->shouldReceive('sendEmailSummary');

		$this->config = $this->sendEmailSummary;
		$this->log = new CapturingLogger;
		$this->emailer = Mockery::mock(Emailer::class);
		$this->emailer->shouldReceive('send');
		$this->pipeline = new Pipeline($this->config, $this->emailer, $this->log);
	});

	test('project with tests that deploys successfully with email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment completed successfully'));

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: Tests passed',
				'INFO: Deployment successful',
				'INFO: Sending email',
			]
		);
	});

	test('project with tests that fail with email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->pipeline->run($project);

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Tests failed'));

		expect($this->log->getoggedLines())->toBe(
			[
				'ERROR: Tests failed',
				'INFO: Sending email',
			]
		);
	});

	test('project without tests that deploys successfully with email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment completed successfully'));

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: No tests',
				'INFO: Deployment successful',
				'INFO: Sending email',
			]
		);
	});

	test('project with tests and failing build with email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment failed'));

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: Tests passed',
				'ERROR: Deployment failed',
				'INFO: Sending email',
			]
		);
	});

	test('project without tests and failing build with email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->pipeline->run($project);

		$this->emailer->expects($this->once())->
			method('send')->
			with($this->equalTo('Deployment failed'));

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: No tests',
				'ERROR: Deployment failed',
				'INFO: Sending email',
			]
		);
	});
});

describe('Pipeline Tests with sendEmailSummary at false', function (): void {

	beforeEach(function (): void {
		$this->sendEmailSummary = Mockery::mock(Config::class);
		$this->sendEmailSummary->shouldReceive('sendEmailSummary')->andReturn(false);
		$this->config = $this->sendEmailSummary;
		$this->log = new CapturingLogger;
		$this->emailer = Mockery::mock(Emailer::class);
		$this->emailer->shouldReceive('send');
		$this->pipeline = new Pipeline($this->config, $this->emailer, $this->log);
	});

	test('project with tests that deploys successfully without email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->pipeline->run($project);

		$this->emailer->expects($this->never())->
			method('send');

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: Tests passed',
				'INFO: Deployment successful',
				'INFO: Email disabled',
			]
		);
	});

	test('project without tests that deploys successfully without email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->pipeline->run($project);

		$this->emailer->expects($this->never())->
			method('send');

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: No tests',
				'INFO: Deployment successful',
				'INFO: Email disabled',
			]
		);
	});

	test('project with tests that fail without email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'ERROR: Tests failed',
				'INFO: Email disabled',
			]
		);
	});

	test('project with tests and failing build without email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: Tests passed',
				'ERROR: Deployment failed',
				'INFO: Email disabled',
			]
		);
	});

	test('project without tests and failing build without email notification', function (): void {
		$project = (Project::builder())->
			setTestStatus(TestStatus::PassingTests)->
			setDeploysSuccessfully(true)->
			build();

		$this->emailer->expects($this->never())->
			method('send');

		$this->pipeline->run($project);

		expect($this->log->getoggedLines())->toBe(
			[
				'INFO: No tests',
				'ERROR: Deployment failed',
				'INFO: Email disabled',
			]
		);
	});
});
