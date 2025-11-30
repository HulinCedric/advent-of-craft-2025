<?php

declare(strict_types = 1);

use People\Person;
use People\Pet;
use People\PetType;

describe('population', function (): void {

	beforeEach(function (): void {
		$this->population = [
			new Person('Peter', 'Griffin', [new Pet(PetType::Cat, 'Tabby', 2)]),
			new Person('Stewie', 'Griffin', [new Pet(PetType::Cat, 'Dolly', 3), new Pet(PetType::Dog, 'Brian', 9)]),
			new Person('Joe', 'Swanson', [new Pet(PetType::Dog, 'Spike', 4)]),
			new Person('Lois', 'Griffin', [new Pet(PetType::Snake, 'Serpy', 1)]),
			new Person('Meg', 'Griffin', [new Pet(PetType::Bird, 'Tweety', 1)]),
			new Person('Chris', 'Griffin', [new Pet(PetType::Turtle, 'Speedy', 4)]),
			new Person('Cleveland', 'Brown', [new Pet(PetType::Hamster, 'Fuzzy', 1), new Pet(PetType::Hamster, 'Wuzzy', 2)]),
			new Person('Glenn', 'Quagmire', []),
		];
	});

	test('Lois owns the youngest pet', function (): void {
		$filtered = $this->population;

		usort($filtered, function ($person1, $person2) {
			$person1PetAge = $person1->pets[0]->age ?? PHP_INT_MAX;
			$person2PetAge = $person2->pets[0]->age ?? PHP_INT_MAX;

			if ($person1PetAge < $person2PetAge) {
				return -1;
			}
			if ($person1PetAge > $person2PetAge) {
				return 1;
			}

			return 0;
		});

		$filtered = reset($filtered);

		expect($filtered)->not->toBeNull();
		expect($filtered->firstName)->toBe('Lois');
	});
});
