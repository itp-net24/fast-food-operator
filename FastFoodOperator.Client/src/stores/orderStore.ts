import { defineStore } from 'pinia'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { ref } from 'vue'
import { CompleteOrder, DeleteOrder, GetOrders, StartOrder } from '@/services/fetcher.ts'

export const orderStore = defineStore('', () => {
  const orders = ref([]);

  const init = async() => {
    orders.value = await GetOrders();

    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:8080/test")
      .configureLogging(LogLevel.Information)
      .build();

    connection.on("ReceiveOrder", (order) => {
      orders.value.push(order);
    });

    connection.on("CompleteOrder", (id: number, status: number) => {
      const order = orders.value.find(o => o.orderId === id);
      order.orderStatus = status;
    });

    connection.on("RemoveOrder", (id: number) => {
      console.log("remove", id);
      removeOrderById(id);
    });

    connection.start().catch(err => console.error(err.toString()));
  }

  const setInProgressAsync = async(order) => {
    try {
      order.orderStatus = 1;
      await StartOrder({ orderId: order.orderId });
    } catch (error) {
      console.error('Fel vid uppdatering av order:', error);
    }
  }

  const setCompleteAsync = async(order) => {
    try {
      order.orderStatus = 2;
      await CompleteOrder({ orderId: order.orderId }); // Pass orderId in an object
    } catch (error) {
      console.error('Fel vid uppdatering av order:', error);
    }
  }

  const deleteOrderAsync = async(order) => {
    if (!confirm(`Are you sure you want to delete #${order.orderNumber}?`))
      return;

    try {
      await DeleteOrder(order.orderId);
      removeOrderById(order.orderId);
    } catch (error) {
      console.error('Fel vid radering av order:', error);
    }
  }

  const removeOrderById = (id: number) => {
    const index = orders.value.findIndex(o => o.orderId === id);
    if (index < 0) return;
    orders.value.splice(index, 1);
  }

  return {
    orders,
    init,
    setInProgressAsync,
    setCompleteAsync,
    deleteOrderAsync,
  }
});
