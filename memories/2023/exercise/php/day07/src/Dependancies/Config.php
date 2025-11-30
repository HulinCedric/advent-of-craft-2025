<?php

declare(strict_types = 1);

namespace App\Dependancies;

interface Config
{
	public function sendEmailSummary(): bool;
}
