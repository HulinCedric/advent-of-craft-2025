<?php

declare(strict_types = 1);

namespace Blog;

use Carbon\Carbon;

class Comment
{
	public function __construct(
		public readonly string $text,
		public readonly string $author,
		public readonly Carbon $creationDate
	) {
	}

	public function isSame(Comment $newComment): bool
	{
		return $this->text === $newComment->text &&
			$this->author === $newComment->author &&
			$this->creationDate->format('Y-m-d') === $newComment->creationDate->format('Y-m-d');
	}
}
