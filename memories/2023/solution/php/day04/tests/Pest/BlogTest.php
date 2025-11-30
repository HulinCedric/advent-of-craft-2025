<?php

declare(strict_types = 1);

use Blog\Article;
use Blog\CommentAlreadyExistException;
use Carbon\Carbon;

beforeEach(function (): void {
	$this->article = new Article(
		'Lorem Ipsum',
		'consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore'
	);
});

describe('Article in a blog', function (): void {
	test('should add valid comment', function (): void {
		$this->article->addComment('Amazing article !!!', 'Pablo Escobar');

		expect(count($this->article->getComments()))->toBeGreaterThan(0);
	});

	test('should add a comment with the given text', function (): void {
		$text = 'Amazing article !!!';
		$this->article->addComment($text, 'Pablo Escobar');

		$comments = $this->article->getComments();
		expect($comments[0]->text)->toBe($text);
	});

	test('should add a comment with the given author', function (): void {
		$author = 'Pablo Escobar';
		$this->article->addComment('Amazing article !!!', $author);

		expect($this->article->getComments()[0]->author)->toBe($author);
	});

	test('should add a comment with the date of the day', function (): void {
		$this->article->addComment('Amazing article !!!', 'Pablo Escobar');
		$firstCommentCreationDate = $this->article->getComments()[0]->creationDate;

		$date = new Carbon;

		expect($firstCommentCreationDate->format('y-m-d'))->toBe($date->format('y-m-d'));
	});

	test('should throw an error when adding existing comment', function (): void {
		$this->article->addComment('Amazing article !!!', 'Pablo Escobar');

		expect(function (): void {
			$this->article->addComment('Amazing article !!!', 'Pablo Escobar');
		})->toThrow(CommentAlreadyExistException::class);
	});
});
