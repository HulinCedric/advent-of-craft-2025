package blog

import org.scalatest.BeforeAndAfterEach
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class ArticleSpec extends AnyFlatSpec with Matchers with BeforeAndAfterEach {

  val AUTHOR: String = "Pablo Escobar"
  val COMMENT_TEXT: String = "Amazing article !!!"
  var article: Article = _

  override def beforeEach(): Unit = {
    article = new Article(
      "Lorem Ipsum",
      "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
    )
  }

  "An Article" should "add a comment" in {
    article.addComment(COMMENT_TEXT, AUTHOR)

    article.getComments should have size 1

    val comment = article.getComments.head
    comment.text shouldBe COMMENT_TEXT
    comment.author shouldBe AUTHOR
  }

  it should "add a comment to an article containing already a comment" in {
    val newComment = "Finibus Bonorum et Malorum"
    val newAuthor = "Al Capone"

    article.addComment(COMMENT_TEXT, AUTHOR)
    article.addComment(newComment, newAuthor)

    article.getComments should have size 2

    val lastComment = article.getComments.last
    lastComment.text shouldBe newComment
    lastComment.author shouldBe newAuthor
  }

  it should "throw CommentAlreadyExistException when adding an existing comment" in {
    article.addComment(COMMENT_TEXT, AUTHOR)

    an[CommentAlreadyExistException] should be thrownBy {
      article.addComment(COMMENT_TEXT, AUTHOR)
    }
  }
}
