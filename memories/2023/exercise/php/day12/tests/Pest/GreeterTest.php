<?php

declare(strict_types = 1);

use Greeting\Greeter;

describe('Greeter', function (): void {

	test('says Hello', function (): void {
		$greeter = new Greeter;

		expect($greeter->greet())->toBe('Hello.');
	});

	test('says Hello Formally', function (): void {
		$greeter = new Greeter;

		$greeter->setFormality('formal');
		expect($greeter->greet())->toBe('Good evening, sir.');
	});

	test('says Hello Casually', function (): void {
		$greeter = new Greeter;

		$greeter->setFormality('casual');
		expect($greeter->greet())->toBe('Sup bro?');
	});

	test('says Hello Intimately', function (): void {
		$greeter = new Greeter;

		$greeter->setFormality('intimate');
		expect($greeter->greet())->toBe('Hello Darling!');
	});
});
