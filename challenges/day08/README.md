# Day 8 - Ugly Code & developer bias.

> **Memory fragment – 2024 · Day 10**  
> That bug was hard to find I recall.  
> Nested loops and the tests...  
> We made it through and I finally fixed it
> ...  
> *I did not have any energy left to refactor.* 

On the eighth day of the journey, we revisit the **memory from 2024**:

That day, we fixed a tricky bug but today, we will take a different approach.
We will imagine you are a tech lead of a team of 4 and you are asking your 3 colleagues to fix the problem for you
AND refactor the code.

We are starting the exercise when you look at the 4 versions (original + 3 fixes).
Hopefully, it will help you recognize when you're experiencing **developer bias** versus spotting real problems.

---

## Challenge — Find your own developer bias

For today's exercise, here is how you would do:

### Part 1: Review All Versions.

Open your chosen language folder and review all 4 Building classes **WITHOUT running tests**.

**For each version, write down:**
1. What bothers you about this code?
2. Would you approve it in code review? (YES/NO)
3. Confidence level: Low / Medium / High

| Version | What bothers you? | Approve? (Y/N) | Confidence |
|---------|-------------------|----------------|------------|
| 1       |                   |                |            |
| 2       |                   |                |            |
| 3       |                   |                |            |
| 4       |                   |                |            |

⚠️ **Important**: Capture your gut reactions BEFORE running any tests!

---

### Part 2: Test Each Version.

Now test each version by changing the import/class name in the test file:

**Java:**
```java
import static delivery.Building.whichFloor;   // Change to Building2, Building3, Building4
```

**C#:**
```csharp
Building.WhichFloor(...)   // Change to Building2, Building3, Building4
```

**Kotlin:**
```kotlin
import delivery.Building   // Change to Building2, Building3, Building4
```

**TypeScript:**
```typescript
import { Building } from "../src/delivery/building";   // Change to building2, building3, building4
```

Run the tests for each version and record the results:

| Version | Tests Passed | Tests Failed | Which test failed? |
|---------|-------------|--------------|-------------------|
| 1       | __/6        |              |                   |
| 2       | __/6        |              |                   |
| 3       | __/6        |              |                   |
| 4       | __/6        |              |                   |

---

### Part 3: Discover what happened?

Before looking at the reveal:

 - Which version(s) did you reject based on style alone?
 - Which version(s) did you approve?
 - Were you confident about which ones had the bug?

---

### Part 4: The Reveal (ONLY WHEN YOU FINISH THE EXERCISE).

<details>
<summary>💡 Click ONLY after you've tested all 4 versions</summary>

## What Each Version Actually Is

- **Version 1 (Building)**: Ugly style + **HAS BUG** ❌
- **Version 2 (Building2)**: Clean style + **HAS SAME BUG** ❌
- **Version 3 (Building3)**: Ugly style + **FIXED** ✅
- **Version 4 (Building4)**: Clean style + **FIXED** ✅
</details>

---

## Where to play

Pick the stack you want to use as your **main stack** this year and open the Day 10 memory from 2024:

- `memories/2024/exercises/<your-stack>/day10`

This is your playground.

---

## Share with the community

On Discord, you can share for example:

 - your results, 
 - the versions, 
 - your bias.

If you want your work to be public, add your file to:

- `community/solutions/dayNN/` by copying the template:  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- then open a pull request.

In `journey/your-name.md`, write a short entry for today:

 - Did the clean versions affect how carefully you reviewed it?
 - Have you ever missed a bug because code "looked professional"?
 - How will this change your approach to code reviews? 

**Happy bias hunting! 🧠**
