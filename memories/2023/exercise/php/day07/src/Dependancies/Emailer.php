<?php

declare(strict_types = 1);

namespace App\Dependancies;

interface Emailer
{
	public function send(string $message): void;
}
