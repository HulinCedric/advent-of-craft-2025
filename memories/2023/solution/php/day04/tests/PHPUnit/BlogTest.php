<?php

declare(strict_types = 1);

namespace Tests;

use Blog\Article;
use Blog\Comment;
use Blog\CommentAlreadyExistException;
use PHPUnit\Framework\TestCase;

class BlogTest extends TestCase
{
	public const AUTHOR = 'Pablo Escobar';

	public const COMMENT_TEXT = 'Amazing article !!!';

	public const FIRST_ARTICLE = 0;

	public const LAST_ARTICLE = 1;

	private Article $article;

	public function setUp(): void
	{
		$this->article = new Article(
			'Lorem Ipsum',
			'consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore'
		);
	}

	public function test_should_add_comment_in_an_article(): void
	{
		$this->article->addComment(self::COMMENT_TEXT, self::AUTHOR);

		$this->assertCount(1, $this->article->getComments());
		$this->assertContainsOnlyInstancesOf(Comment::class, $this->article->getComments());

		$firstComment = $this->article->getComments()[self::FIRST_ARTICLE];

		$this->assertSame(self::COMMENT_TEXT, $firstComment->text);
		$this->assertSame(self::AUTHOR, $firstComment->author);
	}

	public function test_should_add_comment_in_an_article_containing_already_a_comment(): void
	{
		$newComment = 'Finibus Bonorum et Malorum';
		$newAuthor = 'Al Capone';

		$this->article->addComment(self::COMMENT_TEXT, self::AUTHOR);
		$this->article->addComment($newComment, $newAuthor);

		$this->assertCount(2, $this->article->getComments());

		$lastComment = $this->article->getComments()[self::LAST_ARTICLE];

		$this->assertSame($newComment, $lastComment->text);
		$this->assertSame($newAuthor, $lastComment->author);
	}

	public function test_fail_adding_an_existing_comment(): void
	{
		$this->expectException(CommentAlreadyExistException::class);

		$this->article->addComment(self::COMMENT_TEXT, self::AUTHOR);
		$this->article->addComment(self::COMMENT_TEXT, self::AUTHOR);
	}
}
