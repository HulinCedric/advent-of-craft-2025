ThisBuild / version := "0.1.0-SNAPSHOT"

ThisBuild / scalaVersion := "3.3.1"

libraryDependencies += "org.scalatest" %% "scalatest" % "3.2.15" % "test"

libraryDependencies += "eu.monniot" %% "scala3mock" % "0.6.0" % "test"
libraryDependencies += "eu.monniot" %% "scala3mock-scalatest" % "0.6.0" % "test"

lazy val day07 = (project in file("."))
  .settings(
    name := "day07"
  )
