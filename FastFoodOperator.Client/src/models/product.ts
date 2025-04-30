export class Product {
    id: number;
    name: string;
    description: string;
    category: number;
    basePrice: number;
    pictureUrl: string;
<<<<<<< HEAD
    category: number;
=======
>>>>>>> feat/menu

    constructor(id:number, name:string, description:string, basePrice:number, pictureUrl:string, category:number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.basePrice = basePrice;
        this.pictureUrl = pictureUrl;
<<<<<<< HEAD
        this.category = category
=======
        this.category = category;
>>>>>>> feat/menu
    }
}