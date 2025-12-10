# Object Calisthenics Guide

Object Calisthenics are programming exercises formalized as 9 rules by Jeff Bay. These rules focus on maintainability, readability, testability, and comprehensibility of code.

## The 9 Rules

### 1. Only One Level Of Indentation Per Method
Split methods using Extract Method pattern. Reduces complexity and increases readability by breaking nested loops and conditions into separate methods.

### 2. Don't Use The ELSE Keyword
Use early returns, introduce variables for parametrizable returns, or leverage polymorphism with State/Strategy patterns. Avoids nested conditionals.

### 3. Wrap All Primitives And Strings
Encapsulate primitives within objects to avoid Primitive Obsession. If a primitive has behavior, it must be wrapped (e.g., Money, Hour in DDD).

### 4. First Class Collections
Any class containing a collection should contain no other member variables. Wrap collections in dedicated classes where behaviors (filters, rules) have a natural home.

### 5. One Dot Per Line
Don't chain method calls (except for Fluent Interfaces/Method Chaining). Follows Law of Demeter: talk to immediate friends, not strangers.

### 6. Don't Abbreviate
Long names often indicate code duplication or Single Responsibility Principle violations. If you can't find a decent name, something is wrong with the design.

### 7. Keep All Entities Small
No class over 50-150 lines, no package over 10 files. Long files are harder to read, understand, and maintain.

### 8. No Classes With More Than Two Instance Variables
Promotes high cohesion and better encapsulation. Distinguishes between classes that maintain state of a single variable vs. those that coordinate two separate variables.

### 9. No Getters/Setters/Properties
"Tell, Don't Ask" - Make decisions inside the object, not outside. Getters for display purposes are acceptable, but setters violate Open/Closed Principle. Objects should have methods that express intent.

---

*Source: [Object Calisthenics by William Durand](https://williamdurand.fr/2013/06/03/object-calisthenics/)*
