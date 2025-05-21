# Introduction
Previously, the kitchen display relied on polling the API at regular intervals to retrieve all active orders, even though this data was already available on the frontend. As more displays were added, the number of repeated API calls increased significantly, leading to unnecessary load and inefficiency.

To resolve this, I implemented a real-time update mechanism using WebSockets. I chose SignalR for this purpose, as it integrates seamlessly with the existing C# backend, is well-supported, and simplifies real-time communication.

Order updates are now pushed from the server to the client as they occur. Although the update is currently triggered from `OrderService.cs`— rather than `OrderController.cs`— this approach was necessary due to the controller's current responsibility of returning a receipt rather than the created order. This compromise allows the frontend to react immediately to new orders without redundant polling.

## Installation Guide
1. Initialize database
    ```
    dotnet-ef database update
    ```
3. Run API server
4. Start vue server