export class Order {
    id: number;
    orderNumber: number;
    customerNote: string;
    orderStatus: string;
    createdAt: Date;
    startedAt: Date;
    completedAt: Date;

    constructor(id: number, orderNumber: number, customerNote: string, orderStatus: string, createdAt: Date, startedAt: Date, completedAt: Date)  {
        this.id = id;
        this.orderNumber = orderNumber;
        this.customerNote = customerNote;
        this.orderStatus = orderStatus;
        this.createdAt = createdAt;
        this.startedAt = startedAt;
        this.completedAt = completedAt;
    }
}