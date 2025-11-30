<?php

declare(strict_types = 1);

namespace App;

use App\Dependancies\Config;
use App\Dependancies\Emailer;
use App\Dependancies\Logger;
use App\Dependancies\Project;

class Pipeline
{
	public function __construct(
		private readonly Config $config,
		private readonly Emailer $emailer,
		private readonly Logger $log,
	) {
	}

	public function run(Project $project): void
	{
		if ($project->hasTests()) {
			if ('success' === $project->runTests()) {
				$this->log->info('Tests passed');
				$testsPassed = true;
			} else {
				$this->log->error('Tests failed');
				$testsPassed = false;
			}
		} else {
			$this->log->info('No tests');
			$testsPassed = true;
		}

		if ($testsPassed) {
			if ('success' === $project->deploy()) {
				$this->log->info('Deployment successful');
				$deploySuccessful = true;
			} else {
				$this->log->error('Deployment failed');
				$deploySuccessful = false;
			}
		} else {
			$deploySuccessful = false;
		}

		if ($this->config->sendEmailSummary()) {
			$this->log->info('Sending email');
			if ($testsPassed) {
				if ($deploySuccessful) {
					$this->emailer->send('Deployment completed successfully');
				} else {
					$this->emailer->send('Deployment failed');
				}
			} else {
				$this->emailer->send('Tests failed');
			}
		} else {
			$this->log->info('Email disabled');
		}
	}
}
