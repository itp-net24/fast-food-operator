import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { GetOrders, DeleteOrder, StartOrder, CompleteOrder } from '@/services/fetcher.ts'
import { ref } from 'vue'

export default () => {
  const orders = ref([]);

  const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:8080/test")
    .configureLogging(LogLevel.Information)
    .build();

  const initializeAsync = async() => {
    orders.value = await GetOrders();

    connection.on("ReceiveOrder", (order) => {
      orders.value.push(order);
    });

    connection.start().catch(err => console.error(err.toString()));
  }

  const setInProgressAsync = async(order) => {
    try {
      await StartOrder({ orderId: order.orderId });
      order.orderStatus = 1;
    } catch (error) {
      console.error('Fel vid uppdatering av order:', error);
    }
  }

  const setCompleteAsync = async(order) => {
    try {
      await CompleteOrder({ orderId: order.orderId }); // Pass orderId in an object
      order.orderStatus = 2;
    } catch (error) {
      console.error('Fel vid uppdatering av order:', error);
    }
  }

  const deleteOrderAsync = async(order) => {
    if (!confirm(`Are you sure you want to delete #${order.orderNumber}?`))
      return;

    try {
      console.log(order);
      await DeleteOrder(order.orderId);
      removeOrderById(order.orderId);
    } catch (error) {
      console.error('Fel vid radering av order:', error);
    }
  }

  const removeOrderById = (id: number) => {
    const index = orders.value.findIndex(o => o.orderId === id);
    orders.value.splice(index, 1);
  }

  return {
    orders,
    setInProgressAsync,
    setCompleteAsync,
    deleteOrderAsync,
    initializeAsync,
  }
}

export type OrdersComposable = ReturnType<typeof useOrders>;
