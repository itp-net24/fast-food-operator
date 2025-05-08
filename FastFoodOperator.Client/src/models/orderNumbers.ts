export class OrderNumber {
    orderNumber: number;
    orderStatus: string;


    constructor(orderNumber: number, orderStatus: string)  {
        this.orderNumber = orderNumber;
        this.orderStatus = orderStatus;
    }
}