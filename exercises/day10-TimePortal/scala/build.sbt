ThisBuild / version := "1.0-SNAPSHOT"
ThisBuild / scalaVersion := "3.3.1"
ThisBuild / organization := "com.adventofcraft"

lazy val root = (project in file("."))
  .settings(
    name := "time-portal-exercise",
    libraryDependencies ++= Seq(
      "org.scalatest" %% "scalatest" % "3.2.17" % Test,
      "com.tngtech.archunit" % "archunit" % "1.2.1" % Test
    )
  )
