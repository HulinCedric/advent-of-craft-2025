<?php

declare(strict_types = 1);

use Account\Client;

describe('Client ', function (): void {
	beforeEach(function (): void {
		$this->client = new Client([
			'Tenet Deluxe Edition' => 45.99,
			'Inception' => 30.50,
			'The Dark Knight' => 30.50,
			'Interstellar' => 23.98,
		]);
	});

	test('should return statement', function (): void {
		$statement = $this->client->toStatement();

		$expectedStatement = 'Tenet Deluxe Edition for 45.99€' . PHP_EOL .
							 'Inception for 30.5€' . PHP_EOL .
							 'The Dark Knight for 30.5€' . PHP_EOL .
							 'Interstellar for 23.98€' . PHP_EOL .
							 'Total : 130.97€';

		expect($this->client->getTotalAmount())->toBe(130.97);
		expect($statement)->toBe($expectedStatement);
	});
});
