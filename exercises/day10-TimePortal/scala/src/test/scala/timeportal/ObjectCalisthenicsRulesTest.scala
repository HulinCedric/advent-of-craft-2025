package timeportal

import com.tngtech.archunit.core.importer.ClassFileImporter
import com.tngtech.archunit.lang.syntax.ArchRuleDefinition.{noFields, noMethods}
import org.scalatest.BeforeAndAfterAll
import org.scalatest.funspec.AnyFunSpec
import org.scalatest.matchers.should.Matchers

class ObjectCalisthenicsRulesTest extends AnyFunSpec with Matchers with BeforeAndAfterAll:

  private val classes = new ClassFileImporter().importPackages("timeportal")

  override def beforeAll(): Unit =
    println(
      """
        |╔═══════════════════════════════════════════════════════════════╗
        |║  🌀 CHRONOS TEMPORAL GUARDIAN SYSTEM v3.14159                 ║
        |║  ─────────────────────────────────────────────────────────── ║
        |║  Scanning codebase for Object Calisthenics compliance...     ║
        |╚═══════════════════════════════════════════════════════════════╝
        |""".stripMargin)

  describe("⚡ Energy Containment Protocol"):

    it("🔋 Energy levels must be encapsulated in a value object"):
      val rule = noFields()
        .that().haveName("_energyLevel")
        .should().haveRawType(classOf[Int])
        .because("Portal energy requires proper encapsulation to prevent temporal leaks. Create an Energy value object.")

      rule.check(classes)

    it("📅 Year fields must be wrapped in a Year value object"):
      val rule = noFields()
        .that().haveNameMatching(".*[Yy]ear.*")
        .should().haveRawType(classOf[Int])
        .because("Temporal coordinates require Year value objects to enforce boundaries (-10000 to 3000). Create a Year value object.")

      rule.check(classes)

  describe("📜 Temporal Record Keeping"):

    it("📚 Travel logs must be encapsulated in a dedicated TravelLog type"):
      val rule = noFields()
        .that().haveNameMatching(".*[Ll]og.*")
        .should().haveRawType(classOf[scala.collection.mutable.ListBuffer[?]])
        .because("Travel history requires a TravelLog collection class for proper temporal record keeping. Raw ListBuffer exposes internal structure.")

      rule.check(classes)

    it("🌐 Portal collections must be wrapped in a PortalRegistry"):
      val rule = noFields()
        .that().haveName("portals")
        .should().haveRawType(classOf[scala.collection.mutable.ListBuffer[?]])
        .because("Portal networks require a dedicated PortalRegistry for proper management. Raw ListBuffer<TimePortal> lacks domain behavior.")

      rule.check(classes)

  describe("🔒 Collection Integrity Shield"):

    it("🛡️ No method should expose raw List types"):
      val rule = noMethods()
        .that().haveNameMatching("get.*")
        .and().areDeclaredInClassesThat().resideInAPackage("timeportal")
        .should().haveRawReturnType(classOf[List[?]])
        .because("Exposing raw collections allows external temporal interference. Return immutable views or domain-specific types instead.")

      rule.check(classes)

  describe("🎯 Status Type Safety"):

    it("⚡ Portal status must use an enum, not raw String"):
      val rule = noFields()
        .that().haveName("_currentStatus")
        .should().haveRawType(classOf[String])
        .because("Status indicators require type-safe enums. Create a PortalStatus enum with IDLE, CHARGING, JUMPING, ERROR values.")

      rule.check(classes)

    it("❤️ Health status must use an enum, not raw String"):
      val rule = noFields()
        .that().haveName("_healthStatus")
        .should().haveRawType(classOf[String])
        .because("Traveler health requires type-safe status. Create a HealthStatus enum with STABLE, UNSTABLE, CRITICAL values.")

      rule.check(classes)

  describe("🚀 FINAL ACTIVATION SEQUENCE"):

    it("🌀 All systems nominal - Portal ready for temporal operations"):
      val rule = noFields()
        .that().areDeclaredInClassesThat().haveSimpleName("TimePortal")
        .should().haveRawType(classOf[Int])
        .because("TimePortal must have no primitive int fields for stable temporal operations.")

      rule.check(classes)

      println(
        """
          |╔═══════════════════════════════════════════════════════════════╗
          |║  🌀 CHRONOS: ALL SYSTEMS NOMINAL                             ║
          |║  ─────────────────────────────────────────────────────────── ║
          |║  ✅ Energy containment: STABLE                               ║
          |║  ✅ Temporal records: SECURED                                ║
          |║  ✅ Collection integrity: PROTECTED                          ║
          |║  ✅ Status encoding: TYPE-SAFE                               ║
          |║                                                               ║
          |║  🚀 PORTAL ACTIVATION: AUTHORIZED                            ║
          |╚═══════════════════════════════════════════════════════════════╝
          |""".stripMargin)
