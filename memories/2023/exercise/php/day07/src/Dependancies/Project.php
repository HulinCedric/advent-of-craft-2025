<?php

declare(strict_types = 1);

namespace App\Dependancies;

class Project
{
	public function __construct(
		private readonly bool $buildsSuccessfully,
		private readonly TestStatus $testStatus,
	) {
	}

	public static function builder(): ProjectBuilder
	{
		return new ProjectBuilder;
	}

	public function hasTests(): bool
	{
		return TestStatus::NoTests !== $this->testStatus;
	}

	public function runTests(): string
	{
		return TestStatus::PassingTests === $this->testStatus ? 'success' : 'failure';
	}

	public function deploy(): string
	{
		return $this->buildsSuccessfully ? 'success' : 'failure';
	}
}
