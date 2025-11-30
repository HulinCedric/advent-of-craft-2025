ThisBuild / version := "0.1.0-SNAPSHOT"

ThisBuild / scalaVersion := "3.3.1"

libraryDependencies += "org.scalatest" %% "scalatest" % "3.2.15" % "test"

lazy val day22 = (project in file("."))
  .settings(
    name := "day22"
  )
