
export class Product {
    id: number;
    name: string;
    description: string;
    category: number;
    basePrice: number;
    pictureUrl: string;

    constructor(id:number, name:string, description:string, basePrice:number, pictureUrl:string, category:number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.basePrice = basePrice;
        this.pictureUrl = pictureUrl;
        this.category = category
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


