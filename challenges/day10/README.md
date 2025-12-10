# Day 10 - Opening the Time Portal 🌀

> _December 10, 2025 - The Portal Chamber_

![snippet of the day](img/day10.png)

"Nine days of consolidation. Nine days of understanding. But understanding alone won't fix the timeloop."

On the tenth day of the journey, we are moving to the second phase of the adventure. 

You stand before the ancient **CHRONOS system** - the Time Portal that bridges dimensions. Its code is corrupted, its structure unstable. The primitives leak temporal energy. The collections expose their internals to paradox.

**To open the portal to the fractured memories, the code must be restructured.**

The CHRONOS guardian system has encoded the rules as executable tests. **Every rule must pass. Any violation and the portal collapses.**

Today, you refactor reality itself.

---

## Challenge — Make the portal rules pass.

Today, you have a refactoring to do based on a set of Architecture tests. (They are failing at the moment)

The TimePortal class currently violates critical Object Calisthenics principles.

### The rules

 1) You are not allowed to touch anything in the time portal tests.
 2) You can change the behavior tests but you have to keep the behavior working.
 3) You are free to refactor at will as long as all tests are passing.

### Object Calisthenics

They are rules for better designing and reading code. 
There are 9 rules total explained by the book **The ThoughtWorks Anthology** by Jeff Bay.

You have to fix 2 fundamental rules of object calisthenics in this exercise:

**Rule #3: Wrap All Primitives and Strings**
- No naked `int`, `String`, or other primitives
- Every primitive must be wrapped in a domain type
- Enforces validation, adds behavior, makes the domain explicit

**Rule #4: First Class Collections**
- No naked `List<T>` or other raw collections
- Every collection must be wrapped in a domain type
- Collections should have behavior, not just be data bags

[Here](object_calisthenics_guide.md) is a guide for all the rules

Good luck with the portal 

---

## Where to play

For the exploration of the fractured memories, since they are not your own memories
We will go to /exercises and the dayNN

You can then pick the stack you want to use as your **main stack**.

- `exercises/2024/day10/<your-stack>/`

This is your playground.

---

## Share with the community

On Discord, you can share:

 - how do you feel about using object calisthenics to base your refactoring on.

If you want your work to be public, add your file to:

- `community/solutions/dayNN/` by copying the template:  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- then open a pull request.

In `journey/your-name.md`, write a short entry for today:

 - How would you apply object calisthenics in your daily life?
 - In which order did you start applying them?
 - Have you thought of enforcing the rules using ArchUnit testing?

**Let's cross the time portal and rescue memories! 🌀**
