<?php

declare(strict_types = 1);

namespace App\Dependancies;

class ProjectBuilder
{
	private bool $buildsSuccessfully;

	private TestStatus $testStatus;

	public function setTestStatus(TestStatus $testStatus): ProjectBuilder
	{
		$this->testStatus = $testStatus;

		return $this;
	}

	public function setDeploysSuccessfully(bool $buildsSuccessfully): ProjectBuilder
	{
		$this->buildsSuccessfully = $buildsSuccessfully;

		return $this;
	}

	public function build(): Project
	{
		return new Project($this->buildsSuccessfully, $this->testStatus);
	}
}
