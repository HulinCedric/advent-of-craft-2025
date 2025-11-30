<?php

declare(strict_types = 1);

namespace Tests;

use App\Dependancies\Logger;

class CapturingLogger implements Logger
{
	private array $lines = [];

	public function info(string $message): void
	{
		$this->lines[] = 'INFO: ' . $message;
	}

	public function error(string $message): void
	{
		$this->lines[] = 'ERROR: ' . $message;
	}

	public function getLoggedLines(): array
	{
		return $this->lines;
	}
}
