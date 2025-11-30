<?php

declare(strict_types = 1);

namespace App\Dependancies;

enum TestStatus
{
	case NoTests;
	case PassingTests;
	case FailingTests;
}
