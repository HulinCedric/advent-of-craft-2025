# Day 14 - Your tests tell a story

> _December 14, 2025 - Your tests tell a story

![snippet of the day](img/day14.png)

  ==> **Loading Fractured Memory** <==

On the 14th day, your fractured memory takes you to the Toy Quality Assurance department.

The tests work. They verify behavior. But...

Every test setup is 30 lines long. Creating a simple toy for testing requires instantiating parts, configuring assembly rules, setting material properties, defining quality thresholds...

**The setup code drowns the test's intent.**

Your colleague tried to fix this by copying setup code between tests. Now when toy specifications change, you have to update 15 different test files.

## Challenge — Introduce Test Data Builders.

Today is about a simple pattern but it can be a tedious process.

Creating builders will make your test setup readable:
```java
Toy toy = aToy()
    .withName("Wooden Train")
    .forAgeRange(3, 8)
    .build();
```

**Hint:** Start with one builder. Let the pattern emerge. Don't build everything upfront.

---

## Where to play

For the exploration of the fractured memories, since they are not your own memories
We will go to /exercises and the dayNN

You can then pick the stack you want to use as your **main stack**.

- `exercises/day14/<your-stack>/`

This is your playground.

---

## Share with the community

On Discord, you can share:

 - How did your builder help reduce the test complexity?
 - Did you achieve the single point of failure? (one change breaks only one place)

If you want your work to be public, add your file to:

- `community/solutions/dayNN/` by copying the template:  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- then open a pull request.

In `journey/your-name.md`, write a short entry for today:

 - How the builder have helped my tests?
 - How I chained my builders together?

**Let your tests tell the story! 📄**
