# Jour 14 – Tes tests racontent une histoire

> _14 décembre 2025 – Tes tests racontent une histoire_

![snippet of the day](img/day14.png)

  ==> **Chargement de mémoire fracturée** <==

Au 14ᵉ jour, ta mémoire fracturée t’emmène au département de Contrôle Qualité des Jouets.

Les tests fonctionnent. Ils vérifient le comportement. Mais…

Chaque setup de test fait 30 lignes.  
Créer un simple jouet pour les tests demande d’instancier des pièces, de configurer des règles d’assemblage, de définir des propriétés de matériaux, de poser des seuils de qualité…

**Le code de setup noie l’intention du test.**

Ton collègue a essayé de “simplifier” en copiant le setup d’un test à l’autre.  
Maintenant, dès qu’une spécification change, tu dois mettre à jour 15 fichiers de tests différents.

---

## Défi — Introduire des *Test Data Builders*

Aujourd’hui, on travaille un pattern simple, mais le processus peut être un peu fastidieux.

Créer des builders va rendre tes setups de tests lisibles :

```java
Toy toy = aToy()
    .withName("Wooden Train")
    .forAgeRange(3, 8)
    .build();
```

**Indice :** commence par **un seul** builder. Laisse le pattern émerger.  
Ne construis pas tout l’univers des builders dès le départ.

---

## Zone de jeu

Pour l’exploration des mémoires fracturées, comme ce ne sont pas **tes** propres souvenirs,  
nous allons dans `/exercises` et les dayNN.

Tu peux ensuite choisir la stack que tu veux utiliser comme **stack principale** :

- `exercises/day14/<your-stack>/`

C’est ton terrain de jeu.

---

## Partage avec la communauté

Sur Discord, tu peux partager :

- En quoi ton builder a aidé à réduire la complexité de tes tests ?  
- As-tu réussi à te rapprocher du **single point of failure** ? (un changement ne casse qu’un seul endroit)

Si tu veux rendre ton travail public, ajoute ton fichier dans :

- `community/solutions/dayNN/` en copiant le template :  
  `community/solutions/TEMPLATE-[replace_with_your_name].md`  
- puis ouvre une pull request.

Dans `journey/your-name.md`, écris une courte entrée pour aujourd’hui :

- En quoi les builders ont aidé mes tests ?  
- Comment ai-je enchaîné mes builders entre eux ?

**Laisse tes tests raconter l'histoire ! 📄**
