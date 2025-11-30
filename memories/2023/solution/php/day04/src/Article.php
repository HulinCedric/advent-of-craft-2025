<?php

declare(strict_types = 1);

namespace Blog;

use Carbon\Carbon;

class Article
{
	public function __construct(
		private readonly string $name,
		private readonly string $content,
		private array $comments = [],
	) {
	}

	public function addComment(string $text, string $author): void
	{
		$this->addCommentWithCreationDate($text, $author, new Carbon);
	}

	public function getComments(): array
	{
		return $this->comments;
	}

	private function addCommentWithCreationDate(string $text, string $author, Carbon $creationDate): void
	{
		$newComment = new Comment($text, $author, $creationDate);

		foreach ($this->comments as $comment) {
			if ($comment->isSame($newComment)) {
				throw new CommentAlreadyExistException;
			}
		}

		$this->comments[] = $newComment;
	}
}
