using FastFoodOperator.Api.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FastFoodOperator.Api.Hubs;

public class OrderHub() : Hub
{
	public async Task SendOrder(Order order)
	{
		await Clients.All.SendAsync("ReceiveOrder", order);
	}
}