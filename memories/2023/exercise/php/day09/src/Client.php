<?php

declare(strict_types = 1);

namespace Account;

class Client
{
	private int $totalAmount;

	public function __construct(
		private array $orderLines
	) {
	}

	public function toStatement(): string
	{
		$lines = array_map(function ($name, $value) {
			return $this->formatLine($name, $value);
		}, array_keys($this->orderLines), $this->orderLines);

		$statement = implode(PHP_EOL, $lines);
		$statement .= PHP_EOL . 'Total : ' . $this->totalAmount . '€';

		return $statement;
	}

	public function getTotalAmount(): float
	{
		return $this->totalAmount;
	}

	private function formatLine(string $name, float $value): string
	{
		$this->totalAmount += $value;

		return $name . ' for ' . $value . '€';
	}
}
