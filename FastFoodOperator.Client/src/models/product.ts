import type { Tag } from "./interfaces";


export class Product {
    id: number;
    name: string;
    tags: Tag[];
    basePrice: number;
    pictureUrl: string;

    constructor(id:number, name:string, tags: Tag[], basePrice:number, pictureUrl:string, category:number) {
        this.id = id;
        this.name = name;
        this.tags = tags;
        this.basePrice = basePrice;
        this.pictureUrl = pictureUrl;
    }
}

// {
//     "id": 21,
//     "name": "Cappuccino",
//     "description": "Foamy espresso-based coffee",
//     "basePrice": 41,
//     "imageUrl": "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Cappuccino-NEW:nutrition-calculator-tile",
//     "variants": [],
//     "ingredients": []
//   }


