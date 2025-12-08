# Advent of Craft 2025

## Solution DAY 08

- Contributor Discord Name : cedrichulin
- Stack : C#
- Fork : https://github.com/HulinCedric/advent-of-craft-2025/tree/day08

## More feedback to share (optional)

- I loved the experience because: Comparing different code styles and approaches is always interesting. Playing with
  bias is a good exercise. The bias of "nice code = correct code" and _vice-versa_ is something we should be aware.
- I found the exercise could be better if: Knowledge of the business goal to try to catch if the solutions fix the bug
  or not would be great.

### Part 1: Review All Versions

1. What bothers you about this code?
2. Would you approve it in code review? (YES/NO)
3. Confidence level: Low / Medium / High

| Version | What bothers you?                                                                                                                             | Approve? (Y/N) | Confidence |
|---------|-----------------------------------------------------------------------------------------------------------------------------------------------|----------------|------------|
| 1       | Cognitive complexity of 10, too many level of indent, meaningless, not intent, too many code smells, difficult to understand                  | NO             | Low        |
| 2       | Good cognitive complexity, understandable, too imperative, some code smells, can be simpler. Difficult to catch the fix.                      | NO             | Medium     |
| 3       | Cognitive complexity of 9 but better than Version 1. If there is a fix, we cannot easily identify where.                                      | NO             | Medium     |
| 4       | Good cognitive complexity, easily understandable, concise and simpler. More intent. If bug not fixed, it's more on problem understanding side | YES            | High       |

---

### Part 2: Test Each Version

Run the tests for each version and record the results:

| Version | Tests Passed | Tests Failed | Which test failed?         |
|---------|--------------|--------------|----------------------------|
| 1       | 5/6          | 1            | to be 2920, but found 2916 |
| 2       | 5/6          | 1            | to be 2920, but found 2916 |
| 3       | 6/6          | 0            |                            |
| 4       | 6/6          | 0            |                            |

---

### Part 3: Discover what happened?

- Which version(s) did you reject based on style alone?
    - The version 1 & 3 for sure. The 2 partially with some tips to improve.
- Which version(s) did you approve?
    - Verson 4.
- Were you confident about which ones had the bug?
    - The complexity of the original version doesn't help to be confident about fixing (without tests).
    - Version 4 had my highest level of confidence. But not 100% confidence without really knowing what needed to be resolved (to execute the code in my head).