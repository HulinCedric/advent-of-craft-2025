<?php

declare(strict_types = 1);

namespace Games;

class FizzBuzz
{
	private const MIN = 0;

	private const MAX = 100;

	private const FIZZ = 3;

	private const BUZZ = 5;

	private const FIZZBUZZ = 15;

	public function convert(int $input): string | int | OutOfRangeException
	{
		if ($this->isOutOfRange($input)) {
			throw new OutOfRangeException('Input is out of range');
		}

		return $this->convertSafely($input);
	}

	private function convertSafely(int $input): string | int
	{
		return match (true) {
			$this->is(self::FIZZBUZZ, $input) => 'FizzBuzz',
			$this->is(self::FIZZ, $input) => 'Fizz',
			$this->is(self::BUZZ, $input) => 'Buzz',
			default => $input,
		};
	}

	private function is(int $divisor, int $input): bool
	{
		return $input % $divisor === 0;
	}

	private function isOutOfRange(int $input): bool
	{
		return self::MIN >= $input || self::MAX < $input;
	}
}
