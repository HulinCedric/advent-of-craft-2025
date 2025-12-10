package timeportal

import com.tngtech.archunit.core.importer.ClassFileImporter
import com.tngtech.archunit.lang.syntax.ArchRuleDefinition.fields
import com.tngtech.archunit.lang.syntax.ArchRuleDefinition.noFields
import com.tngtech.archunit.lang.syntax.ArchRuleDefinition.noMethods
import org.junit.jupiter.api.BeforeAll
import org.junit.jupiter.api.DisplayName
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test

@DisplayName("🌀 CHRONOS - Time Portal Activation Sequence")
class ObjectCalisthenicsRulesTest {

    companion object {
        private val classes = ClassFileImporter().importPackages("timeportal")

        @BeforeAll
        @JvmStatic
        fun initializeChronos() {
            println("""
                
                ╔═══════════════════════════════════════════════════════════════╗
                ║  🌀 CHRONOS TEMPORAL GUARDIAN SYSTEM v3.14159                 ║
                ║  ─────────────────────────────────────────────────────────── ║
                ║  Scanning codebase for Object Calisthenics compliance...     ║
                ╚═══════════════════════════════════════════════════════════════╝
            """.trimIndent())
        }
    }

    @Nested
    @DisplayName("⚡ Energy Containment Protocol")
    inner class WrapPrimitivesAndStrings {

        @Test
        @DisplayName("🔋 Energy levels must be encapsulated in a value object")
        fun energyLevelsShouldBeWrapped() {
            val rule = noFields()
                .that().haveName("energyLevel")
                .should().haveRawType(Int::class.javaPrimitiveType)
                .because("Portal energy requires proper encapsulation to prevent temporal leaks. Create an Energy value object.")

            rule.check(classes)
        }

        @Test
        @DisplayName("📅 Year fields must be wrapped in a Year value object")
        fun yearsShouldBeWrapped() {
            val rule = noFields()
                .that().haveNameMatching(".*[Yy]ear.*")
                .should().haveRawType(Int::class.javaPrimitiveType)
                .because("Temporal coordinates require Year value objects to enforce boundaries (-10000 to 3000). Create a Year value object.")

            rule.check(classes)
        }
    }

    @Nested
    @DisplayName("📜 Temporal Record Keeping")
    inner class FirstClassCollections {

        @Test
        @DisplayName("📚 Travel logs must be encapsulated in a dedicated TravelLog type")
        fun travelLogsShouldBeFirstClassCollection() {
            val rule = noFields()
                .that().haveNameMatching(".*[Ll]og.*")
                .should().haveRawType(MutableList::class.java)
                .because("Travel history requires a TravelLog collection class for proper temporal record keeping. Raw List exposes internal structure.")

            rule.check(classes)
        }

        @Test
        @DisplayName("🌐 Portal collections must be wrapped in a PortalRegistry")
        fun portalCollectionsShouldBeWrapped() {
            val rule = noFields()
                .that().haveName("portals")
                .should().haveRawType(MutableList::class.java)
                .because("Portal networks require a dedicated PortalRegistry for proper management. Raw List<TimePortal> lacks domain behavior.")

            rule.check(classes)
        }
    }

    @Nested
    @DisplayName("🔒 Collection Integrity Shield")
    inner class NoExposedCollections {

        @Test
        @DisplayName("🛡️ No method should expose raw List types")
        fun shouldNotExposeRawCollections() {
            val rule = noMethods()
                .that().haveNameMatching("get.*")
                .and().areDeclaredInClassesThat().resideInAPackage("timeportal")
                .should().haveRawReturnType(List::class.java)
                .because("Exposing raw collections allows external temporal interference. Return immutable views or domain-specific types instead.")

            rule.check(classes)
        }
    }

    @Nested
    @DisplayName("🎯 Status Type Safety")
    inner class StatusEncapsulation {

        @Test
        @DisplayName("⚡ Portal status must use an enum, not raw String")
        fun portalStatusShouldNotBeRawString() {
            val rule = noFields()
                .that().haveName("currentStatus")
                .should().haveRawType(String::class.java)
                .because("Status indicators require type-safe enums. Create a PortalStatus enum with IDLE, CHARGING, JUMPING, ERROR values.")

            rule.check(classes)
        }

        @Test
        @DisplayName("❤️ Health status must use an enum, not raw String")
        fun healthStatusShouldBeEncapsulated() {
            val rule = noFields()
                .that().haveName("healthStatus")
                .should().haveRawType(String::class.java)
                .because("Traveler health requires type-safe status. Create a HealthStatus enum with STABLE, UNSTABLE, CRITICAL values.")

            rule.check(classes)
        }
    }

    @Nested
    @DisplayName("🚀 FINAL ACTIVATION SEQUENCE")
    inner class PortalActivation {

        @Test
        @DisplayName("🌀 All systems nominal - Portal ready for temporal operations")
        fun allSystemsNominal() {
            val rule = noFields()
                .that().areDeclaredInClassesThat().haveSimpleName("TimePortal")
                .should().haveRawType(Int::class.javaPrimitiveType)
                .because("TimePortal must have no primitive int fields for stable temporal operations.")

            rule.check(classes)

            println("""
                
                ╔═══════════════════════════════════════════════════════════════╗
                ║  🌀 CHRONOS: ALL SYSTEMS NOMINAL                             ║
                ║  ─────────────────────────────────────────────────────────── ║
                ║  ✅ Energy containment: STABLE                               ║
                ║  ✅ Temporal records: SECURED                                ║
                ║  ✅ Collection integrity: PROTECTED                          ║
                ║  ✅ Status encoding: TYPE-SAFE                               ║
                ║                                                               ║
                ║  🚀 PORTAL ACTIVATION: AUTHORIZED                            ║
                ╚═══════════════════════════════════════════════════════════════╝
            """.trimIndent())
        }
    }
}
