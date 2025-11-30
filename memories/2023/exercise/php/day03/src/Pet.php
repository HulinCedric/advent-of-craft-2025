<?php

declare(strict_types = 1);

namespace People;

class Pet
{
	public function __construct(
		public readonly PetType $type,
		public readonly string $name,
		public readonly int $age
	) {
	}
}
