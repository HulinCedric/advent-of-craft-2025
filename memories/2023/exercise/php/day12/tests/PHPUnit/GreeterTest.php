<?php

declare(strict_types = 1);

use Greeting\Greeter;
use PHPUnit\Framework\TestCase;

class GreeterTest extends TestCase
{
	public function test_says_hello(): void
	{
		$greeter = new Greeter;

		$this->assertSame('Hello.', $greeter->greet());
	}

	public function test_says_hello_formally(): void
	{
		$greeter = new Greeter;
		$greeter->setFormality('formal');

		$this->assertSame('Good evening, sir.', $greeter->greet());
	}

	public function test_says_hello_casually(): void
	{
		$greeter = new Greeter;
		$greeter->setFormality('casual');

		$this->assertSame('Sup bro?', $greeter->greet());
	}

	public function test_says_hello_intimately(): void
	{
		$greeter = new Greeter;
		$greeter->setFormality('intimate');

		$this->assertSame('Hello Darling!', $greeter->greet());
	}
}
