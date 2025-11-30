package blog

import java.time.LocalDate
import scala.collection.mutable.ListBuffer

class Article(val name: String, val content: String) {
  private val comments = ListBuffer[Comment]()

  private def addComment(text: String, author: String, creationDate: LocalDate): Unit = {
    val comment = Comment(text, author, creationDate)

    if (comments.contains(comment)) {
      throw new CommentAlreadyExistException
    } else {
      comments += comment
    }
  }

  def addComment(text: String, author: String): Unit =
    addComment(text, author, LocalDate.now())

  def getComments: List[Comment] = comments.toList
}

