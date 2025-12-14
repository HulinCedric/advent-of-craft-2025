# Advent Of Craft 2025
[![Our Discord](https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white)](https://discord.gg/E5Z9s9UKTS)
[![Linkedin](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/company/advent-of-craft)
[![Youtube channel](https://camo.githubusercontent.com/94b947e758f767a15576edfb06cc06075d6b62ef7a8946db69c5ce4a2ee830f7/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f596f75547562652d4646303030303f7374796c653d666f722d7468652d6261646765266c6f676f3d796f7574756265266c6f676f436f6c6f723d7768697465)](https://www.youtube.com/@AdventOfCraft)

Join us on [`Discord`](https://discord.gg/E5Z9s9UKTS) and [`Linkedin`](https://www.linkedin.com/company/advent-of-craft) to follow the initiative.

> Time loop edition: revisiting old exercises as **memories**, unlocking them with **Key Activations**, and documenting your journey.

![Advent Of Craft 2025](img/AOC-2025.png)

## Table of Contents

- [What is this edition about?](#what-is-this-edition-about)
  - [The trilogy so far](#the-trilogy-so-far)
- [How 2025 works](#how-2025-works)
  - [Memories](#memories)
  - [Key Activations](#key-activations)
  - [Your journey](#your-journey)  
  - [How to participate](#how-to-participate)
- [Who are we?](#who-are-we)

---

## What is this edition about?

Christmas is closing in again. You’ve already survived a couple of December adventures, refactored some questionable code, and probably shipped features you’re still not proud of.

This year, instead of yet another “new exercise every day”, we’re doing something different:

- We **reuse** the exercises from the 2023 and 2024 editions.  
- We treat them as **memories** from previous loops.  
- Each day, you get:
  - a short **memory fragment** (a narrative echo of an old challenge),  
  - a **Key Activation**: a focused call to action (refactoring, testing, design, tooling, collaboration, etc.),  
  - and the space to explore and discuss everything on **Discord**.

The goal is not to finish all the exercises. The goal is to **unlock memories**, experiment with new angles on familiar code, and grow your craft with others.

### The trilogy so far

- **Advent of Craft 2023**  
  The original journey into software craftsmanship: daily exercises on refactoring, testing, design, and more, each with a proposed solution and step-by-step guide.  
  → [2023 repository](https://github.com/advent-of-craft/2023)

- **Advent of Craft 2024 - North Star Solutions**  
  A narrative-driven calendar set in the world of *North Star Solutions*, following a consultant through increasingly complex challenges in multiple languages.  
  → [2024 repository](https://github.com/advent-of-craft/2024)

- **Advent of Craft 2025 - Time loop & memories**  
  This edition **reuses** the 2023/2024 challenges as memories, adds new **Key Activations**, and focuses on reflection, experimentation, and community discussion rather than daily solutions.

---

## How 2025 works

Instead of “Day N = brand new kata + solution tomorrow”, this year looks like:

- You work inside a **single repo**: this one (`2025`).
- 2023 and 2024 exercises live under `memories/` as **snapshots**.
- Each day you read an **activation** file with:
  - a memory fragment (short story / context),
  - a **Key Activation** (what to actually do),
  - pointers to the relevant memory code.
- You keep track of your own **journey** in a file under `journey/`.
- Discussions, questions, screenshots, and sharing all happen on **Discord**.

### Memories

All the past code is gathered under:

- `memories/2023/...` - curated snapshot of Advent of Craft 2023  
- `memories/2024/...` - curated snapshot of Advent of Craft 2024

They keep (as much as possible) the original structure:

```text
memories/
  2023/
    exercises/
      <stack>/
        day01/
        day02/
        ...
  2024/
    exercises/
      <stack>/
        day01/
        day02/
        ...
```

### Key Activations

In 2025, there is no new “all-inclusive exercise per day”.

Instead, each day has one main Key Activation, for example:
- “Base your refactoring on complexity metrics.”
- “Do a cross-review with another participant’s fork.”
- etc

You’ll find them under challenges/dayNN/README.md, along with the memory fragment and links to the matching code in memories/.

You choose how deep to go: do a 30-minute pass just to poke at the code,
or turn it into a multi-hour deep dive if you want.

#### Push your solution onto the repository

You can push your solution in the /community/solutions folder by copying the template (in /community/solutions/TEMPLATE-[replace-with-your-name].md) in the corresponding day and fill it up with your name and fork information. 

```text
community/
  solutions/
  TEMPLATE-[replace-with-your-name].md //to copy and fill
    day00/
      paul.md //paste it here
    day01/
      ...
    ...
```

### Your journey

We strongly encourage you to keep a journey log in the repository:

Copy journey/TEMPLATE.md to journey/\<your-name>.md.

After each day you participate, jot down:
 - which memory you worked on,
 - which Key Activation you picked,
- what you discovered (in the code, in your habits, in your biases),
 - anything you want Future-You to remember.

### How to participate
[Here are the instructions for your journey](challenges/INSTRUCTIONS.md)

## Key Activations
- [Day 00 - Warming up](challenges/day00/README.md)
- [Day 01 - Complexity as a compass.](challenges/day01/README.md)
- [Day 02 - Change your stack & IDE.](challenges/day02/README.md)
- [Day 03 - SOLID from another angle.](challenges/day03/README.md)
- [Day 04 - Review the solution.](challenges/day04/README.md)
- [Day 05 - Battle of TDD, Human vs AI.](challenges/day05/README.md)
- [Day 06 - Carve a pure pipeline.](challenges/day06/README.md)
- [Day 07 - Bug Driven Development.](challenges/day07/README.md)
- [Day 08 - Ugly Code & developer bias.](challenges/day08/README.md)
- [Day 09 - A conventional journey.](challenges/day09/README.md)
- [Day 10 - Opening the Time Portal.](challenges/day10/README.md)
- [Day 11 - A world without mock](challenges/day11/README.md)
- [Day 12 - Human password policy.](challenges/day12/README.md)
- [Day 13 - Keyboard-first refactoring.](challenges/day13/README.md)
- [Day 14 - Your tests tell a story](challenges/day14/README.md)

## Who are we?

Advent of Craft is an initiative run by a small group of software crafters who care about:
 - long-lived codebases,
 - learning in public,
 - and making December a time for reflection as well as fun.

The core team and many contributors are the same as in previous editions.
You can find them (and maybe yourself) in:
 - the Contributors section of the 2023 and 2024 repositories,
 - the “Founders / Contributors” sections and visuals there,
 - and of course on Discord.

### Founders

<table>
<tr>
    <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/yanncourtel>
            <img src=https://avatars.githubusercontent.com/u/75068587?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Yann Courtel/>
            <br />
            <sub style="font-size:14px"><b>Yann Courtel</b></sub>
        </a>
    </td>
        <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/LaurentDecamps>
            <img src=https://avatars.githubusercontent.com/u/3742803?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Yoan Thirion/>
            <br />
            <sub style="font-size:14px"><b>Laurent Decamps</b></sub>
        </a>
    </td>
</tr>
</table>

### Contributors
<table>
<tr>
    <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/ythirion>
            <img src=https://avatars.githubusercontent.com/u/20967693?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Yoan Thirion/>
            <br />
            <sub style="font-size:14px"><b>Yoan Thirion</b></sub>
        </a>
    </td>
    <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/tr00d>
            <img src=https://avatars.githubusercontent.com/u/59444272?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Guillaume Faas/>
            <br />
            <sub style="font-size:14px"><b>Guillaume Faas</b></sub>
        </a>
    </td>
    <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/Dyshay>
            <img src=https://avatars.githubusercontent.com/u/35565100?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Dylan Dyshay/>
            <br />
            <sub style="font-size:14px"><b>Dylan Dyshay</b></sub>
        </a>
    </td>
    <td align="center" style="word-wrap: break-word; width: 150.0; height: 150.0">
        <a href=https://github.com/pierrebelin>
            <img src=https://avatars.githubusercontent.com/u/25244392?v=4 width="100;"  style="border-radius:50%;align-items:center;justify-content:center;overflow:hidden;padding-top:10px" alt=Pierre Belin/>
            <br />
            <sub style="font-size:14px"><b>Pierre Belin</b></sub>
        </a>
    </td>
</tr>
</table>
