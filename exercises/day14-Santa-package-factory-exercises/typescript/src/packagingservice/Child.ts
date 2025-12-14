import { ChildGender } from "./enums";
import { Gift } from "./Gift";

export class Child {
  readonly name: string;
  readonly age: number;
  readonly gender: ChildGender;
  readonly hasBeenNice: boolean;
  readonly assignedGift: Gift;

  constructor(
    name: string,
    age: number,
    gender: ChildGender,
    hasBeenNice: boolean,
    assignedGift: Gift
  ) {
    this.name = name;
    this.age = age;
    this.gender = gender;
    this.hasBeenNice = hasBeenNice;
    this.assignedGift = assignedGift;
  }
}
