export class Product {
    id: number;
    name: string;
    description: string;
    basePrice: number;
    pictureUrl: string;
    category: number;

    constructor(id:number, name:string, description:string, basePrice:number, pictureUrl:string, category:number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.basePrice = basePrice;
        this.pictureUrl = pictureUrl;
        this.category = category
    }
}