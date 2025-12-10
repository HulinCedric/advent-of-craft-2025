# Jour 10 – Ouverture du Portail Temporel 🌀

> _10 décembre 2025 – Salle du Portail_

![snippet of the day](img/day10.png)

« Neuf jours de consolidation. Neuf jours à comprendre. Mais la compréhension seule ne suffira pas à réparer la boucle temporelle. »

Au dixième jour du voyage, nous entrons dans la **seconde phase de l’aventure**.

Tu te tiens devant l’ancien système **CHRONOS** – le Portail Temporel qui relie les dimensions.  
Son code est corrompu, sa structure instable. Les primitifs fuient de l’énergie temporelle. Les collections exposent leurs entrailles aux paradoxes.

**Pour ouvrir le portail vers les mémoires fracturées, le code doit être restructuré.**

Le système gardien de CHRONOS a encodé les règles sous forme de tests exécutables.  
**Toutes les règles doivent passer. À la moindre violation, le portail s’effondre.**

Aujourd’hui, tu refactores la réalité elle-même.

---

## Défi — Faire respecter les règles du portail

Aujourd’hui, tu as un refactoring à réaliser à partir d’un ensemble de tests d’architecture.  
(Actuellement, ils échouent.)

La classe `TimePortal` viole des principes critiques des **Object Calisthenics**.

### Les règles

1. Tu n’as pas le droit de toucher quoi que ce soit dans les tests du portail temporel.  
2. Tu peux modifier les tests de comportement, mais tu dois conserver le même comportement fonctionnel.  
3. Tu es libre de refactorer comme tu veux tant que **tous les tests passent**.

---

### Object Calisthenics

Ce sont des règles pour mieux concevoir et lire le code.  
Il y a 9 règles au total, expliquées dans le livre **The ThoughtWorks Anthology** de Jeff Bay.

Dans cet exercice, tu dois corriger **2 règles fondamentales** des Object Calisthenics :

**Règle n°3 : Envelopper tous les primitifs et les chaînes de caractères**  
- Pas de `int`, `String` ou autres primitifs « nus »  
- Chaque primitif doit être encapsulé dans un type de domaine  
- Cela force la validation, ajoute du comportement, et rend le domaine explicite  

**Règle n°4 : Collections de première classe**  
- Pas de `List<T>` ou autre collection « nue »  
- Chaque collection doit être encapsulée dans un type de domaine  
- Une collection doit porter du comportement, pas seulement des données  

Un guide complet sur toutes les règles se trouve [ici](./object-calisthenics-guide.md).

Bonne chance avec le portail. ⏳

---

## Zone de jeu

Pour l’exploration des mémoires fracturées, comme ce ne sont pas **tes** propres souvenirs,  
nous n’allons pas dans `memories/` mais dans `/exercises`, jour par jour.

Tu peux ensuite choisir la stack que tu veux utiliser comme **stack principale** :

- `exercises/2024/day10/<your-stack>/`

C’est ton terrain de jeu.

---

## Partage avec la communauté

Sur Discord, tu peux partager par exemple :

- comment tu te sens en basant ton refactoring sur les Object Calisthenics.

Si tu veux rendre ton travail public, ajoute ton fichier dans :

- `community/solutions/dayNN/` en copiant le template :  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- puis ouvre une pull request.

Dans `journey/your-name.md`, écris une courte entrée pour aujourd’hui :

- Comment pourrais-tu appliquer les Object Calisthenics dans ton quotidien ?  
- Dans quel ordre as-tu commencé à appliquer ces règles ?  
- As-tu déjà envisagé de faire respecter ces règles avec des tests de type ArchUnit ?

**Traversons le portail temporel et sauvons les mémoires ! 🌀**
