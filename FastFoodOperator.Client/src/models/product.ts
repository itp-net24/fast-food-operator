export class Product {
    id: number;
    name: string;
    description: string;
    basePrice: number;
    pictureUrl: string;

    constructor(id:number, name:string, description:string, basePrice:number, pictureUrl:string) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.basePrice = basePrice;
        this.pictureUrl = pictureUrl;
    }
}