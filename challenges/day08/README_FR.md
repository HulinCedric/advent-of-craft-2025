# Jour 08 – Code moche & biais de développeur

> **Fragment de mémoire – 2024 · Jour 10**  
> Je me souviens que ce bug avait été difficile à trouver.  
> Des boucles imbriquées et les tests…  
> On a fini par y arriver et j’ai enfin corrigé le problème.  
> ...  
> *Je n’avais plus aucune énergie pour refactorer.* 

Pour ce huitième jour du voyage, nous revisitons la **mémoire de 2024**.

Ce jour-là, nous avons corrigé un bug délicat.  
Aujourd’hui, nous allons prendre une approche différente.

Nous allons imaginer que tu es tech lead d’une équipe de 4 personnes,  
et que tu as demandé à tes 3 collègues de corriger le problème **et** de refactorer le code.

L’exercice commence au moment où tu regardes les 4 versions de code (l'original et les 3 fixes).  
L’objectif est de t’aider à reconnaître quand tu es en train de vivre un **biais de développeur**,  
par opposition à de vrais problèmes dans le code.

---

## Défi — Identifier ton propre biais de développeur

Pour l’exercice d’aujourd’hui, voici comment procéder :

### Partie 1 : Relire toutes les versions

Ouvre le dossier de ton langage et examine les 4 versions de la classe `Building`  
**SANS lancer les tests**.

**Pour chaque version, note :**

1. Qu’est-ce qui te dérange dans ce code ?  
2. Est-ce que tu l’approuverais en revue de code ? (OUI/NON)  
3. Ton niveau de confiance : Faible / Moyen / Élevé  

| Version | Qu’est-ce qui te dérange ? | Approve ? (O/N) | Confiance |
|---------|----------------------------|------------------|-----------|
| 1       |                            |                  |           |
| 2       |                            |                  |           |
| 3       |                            |                  |           |
| 4       |                            |                  |           |

⚠️ **Important** : capture tes réactions à chaud AVANT d’exécuter les tests !

---

### Partie 2 : Tester chaque version

Ensuite, teste chaque version en changeant l’import / le nom de classe dans le fichier de test :

**Java :**
```java
import static delivery.Building.whichFloor;   // Change to Building2, Building3, Building4
```

**C# :**
```csharp
Building.WhichFloor(...)   // Change to Building2, Building3, Building4
```

**Kotlin :**
```kotlin
import delivery.Building   // Change to Building2, Building3, Building4
```

**TypeScript :**
```typescript
import { Building } from "../src/delivery/building";   // Change to building2, building3, building4
```

Lance les tests pour chaque version et note les résultats :

| Version | Tests réussis | Tests en échec | Quel test a échoué ? |
|---------|---------------|----------------|-----------------------|
| 1       | __/6          |                |                       |
| 2       | __/6          |                |                       |
| 3       | __/6          |                |                       |
| 4       | __/6          |                |                       |

---

### Partie 3 : Découvrir ce qui s’est passé

Avant de passer à la partie révelation, regarde le code et enquête sur ce qu’il s’est passé.

Parmi les trois versions proposées,  
**laquelle as-tu rejetée spontanément ? Laquelle as-tu préférée ?**

Essaie de faire la part entre :

- ce qui relève de ton **goût personnel / biais**,  
- et ce qui relève de vrais problèmes de comportement ou de lisibilité.

---

### Partie 4 : La révélation (UNIQUEMENT QUAND TU AS FINI L’EXERCICE)

<details>
<summary>💡 Cliquer UNIQUEMENT après avoir testé les 4 versions</summary>

## Ce que sont vraiment les versions

- **Version 1 (Building)** : Style “moche” + **CONTIENT UN BUG** ❌  
- **Version 2 (Building2)** : Style “propre” + **CONTIENT LE MÊME BUG** ❌  
- **Version 3 (Building3)** : Style “moche” + **BUG CORRIGÉ** ✅  
- **Version 4 (Building4)** : Style “propre” + **BUG CORRIGÉ** ✅  

</details>

---

## Zone de jeu

Choisis la stack que tu veux utiliser comme **stack principale** cette année et ouvre la mémoire du Jour 10 de 2024 :

- `memories/2024/exercises/<your-stack>/day10`

C’est ton terrain de jeu.

---

## Partage avec la communauté

Sur Discord, tu peux partager par exemple :

- tes résultats,  
- les versions que tu as préférées / rejetées,  
- les biais que tu as identifiés chez toi.

Si tu veux rendre ton travail public, ajoute ton fichier dans :

- `community/solutions/dayNN/` en copiant le template :  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- puis ouvre une pull request.

Dans `journey/your-name.md`, écris une courte entrée pour aujourd’hui :

- Est-ce que les versions “propres” ont influencé la façon dont tu les as relues ?  
- As-tu déjà manqué un bug parce que le code “avait l’air pro” ?  
- Qu’est-ce que cela va changer dans ta façon d’aborder les revues de code ?  

**Bonne chasse aux biais ! 🧠**
