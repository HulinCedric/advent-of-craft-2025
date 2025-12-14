import { GiftSize } from "./enums";

export class Gift {
  readonly name: string;
  readonly size: GiftSize;
  readonly isFragile: boolean;
  readonly recommendedMinAge: number;

  constructor(
    name: string,
    size: GiftSize,
    isFragile: boolean,
    recommendedMinAge: number
  ) {
    this.name = name;
    this.size = size;
    this.isFragile = isFragile;
    this.recommendedMinAge = recommendedMinAge;
  }
}
