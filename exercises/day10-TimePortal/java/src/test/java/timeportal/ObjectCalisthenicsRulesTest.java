package timeportal;

import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import com.tngtech.archunit.lang.ArchRule;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import java.util.List;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.fields;
import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.methods;
import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.noFields;
import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.noMethods;

@DisplayName("🌀 CHRONOS - Time Portal Activation Sequence")
class ObjectCalisthenicsRulesTest {

    private static JavaClasses classes;

    @BeforeAll
    static void initializeChronos() {
        System.out.println("""
            
            ╔═══════════════════════════════════════════════════════════════╗
            ║  🌀 CHRONOS TEMPORAL GUARDIAN SYSTEM v3.14159                 ║
            ║  ─────────────────────────────────────────────────────────── ║
            ║  Scanning codebase for Object Calisthenics compliance...     ║
            ╚═══════════════════════════════════════════════════════════════╝
            """);
        
        classes = new ClassFileImporter()
                .importPackages("timeportal");
    }

    @Nested
    @DisplayName("⚡ Energy Containment Protocol")
    class WrapPrimitivesAndStrings {

        @Test
        @DisplayName("🔋 Energy levels must be encapsulated in a value object")
        void energyLevelsShouldBeWrapped() {
            ArchRule rule = noFields()
                    .that().haveName("energyLevel")
                    .should().haveRawType(int.class)
                    .because("Portal energy requires proper encapsulation to prevent temporal leaks. " +
                            "Create an Energy value object.");

            rule.check(classes);
        }

        @Test
        @DisplayName("📅 Year fields must be wrapped in a Year value object")
        void yearsShouldBeWrapped() {
            ArchRule rule = noFields()
                    .that().haveNameMatching(".*[Yy]ear.*")
                    .should().haveRawType(int.class)
                    .because("Temporal coordinates require Year value objects to enforce boundaries (-10000 to 3000). " +
                            "Create a Year value object.");

            rule.check(classes);
        }
    }

    @Nested
    @DisplayName("📜 Temporal Record Keeping")
    class FirstClassCollections {

        @Test
        @DisplayName("📚 Travel logs must be encapsulated in a dedicated TravelLog type")
        void travelLogsShouldBeFirstClassCollection() {
            ArchRule rule = noFields()
                    .that().haveNameMatching(".*[Ll]og.*")
                    .should().haveRawType(List.class)
                    .because("Travel history requires a TravelLog collection class for proper temporal record keeping. " +
                            "Raw List exposes internal structure.");

            rule.check(classes);
        }

        @Test
        @DisplayName("🌐 Portal collections must be wrapped in a PortalRegistry")
        void portalCollectionsShouldBeWrapped() {
            ArchRule rule = noFields()
                    .that().haveName("portals")
                    .should().haveRawType(List.class)
                    .because("Portal networks require a dedicated PortalRegistry for proper management. " +
                            "Raw List<TimePortal> lacks domain behavior.");

            rule.check(classes);
        }
    }

    @Nested
    @DisplayName("🔒 Collection Integrity Shield")
    class NoExposedCollections {

        @Test
        @DisplayName("🛡️ No method should expose raw List types")
        void shouldNotExposeRawCollections() {
            ArchRule rule = noMethods()
                    .that().haveNameMatching("get.*")
                    .and().areDeclaredInClassesThat().resideInAPackage("timeportal")
                    .should().haveRawReturnType(List.class)
                    .because("Exposing raw collections allows external temporal interference. " +
                            "Return immutable views or domain-specific types instead.");

            rule.check(classes);
        }
    }

    @Nested
    @DisplayName("🎯 Status Type Safety")
    class StatusEncapsulation {

        @Test
        @DisplayName("⚡ Portal status must use an enum, not raw String")
        void portalStatusShouldNotBeRawString() {
            ArchRule rule = noFields()
                    .that().haveName("currentStatus")
                    .should().haveRawType(String.class)
                    .because("Status indicators require type-safe enums. " +
                            "Create a PortalStatus enum with IDLE, CHARGING, JUMPING, ERROR values.");

            rule.check(classes);
        }

        @Test
        @DisplayName("❤️ Health status must use an enum, not raw String")
        void healthStatusShouldBeEncapsulated() {
            ArchRule rule = noFields()
                    .that().haveName("healthStatus")
                    .should().haveRawType(String.class)
                    .because("Traveler health requires type-safe status. " +
                            "Create a HealthStatus enum with STABLE, UNSTABLE, CRITICAL values.");

            rule.check(classes);
        }
    }

    @Nested
    @DisplayName("🚀 FINAL ACTIVATION SEQUENCE")
    class PortalActivation {

        @Test
        @DisplayName("🌀 All systems nominal - Portal ready for temporal operations")
        void allSystemsNominal() {
            // Verify no primitive int fields remain in TimePortal
            ArchRule noPrimitiveInts = noFields()
                    .that().areDeclaredInClassesThat().haveSimpleName("TimePortal")
                    .should().haveRawType(int.class)
                    .because("TimePortal must have no primitive int fields for stable temporal operations.");

            noPrimitiveInts.check(classes);

            System.out.println("""
                
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
                """);
        }
    }
}
