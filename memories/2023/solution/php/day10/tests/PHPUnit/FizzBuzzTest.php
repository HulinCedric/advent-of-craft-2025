<?php

declare(strict_types = 1);

use Games\FizzBuzz;
use Games\OutOfRangeException;
use PHPUnit\Framework\TestCase;

class FizzBuzzTest extends TestCase
{
	private $fizzBuzz;

	protected function setUp(): void
	{
		$this->fizzBuzz = new FizzBuzz;
	}

	public static function validInputs()
	{
		return [
			[1, '1'],
			[67, '67'],
			[82, '82'],
			[3, 'Fizz'],
			[66, 'Fizz'],
			[99, 'Fizz'],
			[5, 'Buzz'],
			[50, 'Buzz'],
			[85, 'Buzz'],
			[15, 'FizzBuzz'],
			[30, 'FizzBuzz'],
			[45, 'FizzBuzz'],
		];
	}

	public static function invalidInputs()
	{
		return [
			[0],
			[-1],
			[101],
		];
	}

	/**
	 * @dataProvider validInputs
	 */
	public function test_returns_number_representation(int $input, string $expectedResult): void
	{
		$this->assertSame($expectedResult, $this->fizzBuzz->convert($input));
	}

	/**
	 * @dataProvider invalidInputs
	 */
	public function test_throws_an_exception_for_numbers_out_of_range(int $input): void
	{
		$this->expectException(OutOfRangeException::class);
		$this->fizzBuzz->convert($input);
	}
}
