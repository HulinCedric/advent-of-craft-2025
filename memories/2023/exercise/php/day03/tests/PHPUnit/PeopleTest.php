<?php

declare(strict_types = 1);

use People\Person;
use People\Pet;
use People\PetType;
use PHPUnit\Framework\TestCase;

class PopulationTest extends TestCase
{
	private array $population;

	protected function setUp(): void
	{
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
	}

	public function test_lois_owns_the_youngest_pet(): void
	{
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

		$youngestPetOwner = reset($filtered);

		$this->assertNotNull($youngestPetOwner);
		$this->assertSame('Lois', $youngestPetOwner->firstName);
	}
}
