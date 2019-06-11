# WasteMan
A solid waste management dashboard. It utilizes the Message Queuing Telemetry Transport (MQTT) Protocol to receive data from the IoT-based garbage bins from a broker. The Shortest Path First (SPF) and Depth-First Search (DFS) algorithm was used to determine the shortest collection route to the full garbage bins. Lastly, Websockets enabled the transimission of data over the client in real-time. 

WasteMan is an implementation of the researches entitled:
- _IoT-based Garbage Bin for Solid Waste Collection with Determination of Route
   Using the Shortest Path First Algorithm_
- _Determination of Solid Waste Collection Routes by Graph Theory with Graph Minimization 
   Using Transitive Reduction for an IoT-based Garbage Bin_

__Research Status__
- [x] Peer Review
- [x] Proofread
- [x] Local Colloquium
- [ ] Conference - On proceeding

## Architecture
![WasteMan Architecture](https://github.com/IanEscober/WasteMan/blob/master/docs/Architecture.png)
- [WasteMan](https://github.com/IanEscober/WasteMan) - Server
- [WasteMan-Client](https://github.com/IanEscober/WasteMan-Client) - Client
- [WasteMan-Driver](https://github.com/IanEscober/WasteMan-Driver) - Garbage bin

## Technologies/Libraries/Frameworks
### Protocol
- [MQTT](http://mqtt.org/) - Publishing/Subscribing of data across the IoT devices
    - [MQTTnet](https://github.com/chkr1011/MQTTnet) 
### Database
- [MongoDB](https://www.mongodb.com/) - Persistence of algorithm results
    - [mongo-chsarp-driver](https://github.com/mongodb/mongo-csharp-driver) 
- [Redis](https://redis.io/) - Caching of garbage bins data
    - [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis)
### Transmission
- [WebSockets](https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API) - Real-time transmission of data to the client
    - [SignalR](https://github.com/SignalR/SignalR) 
### API
- [ASP.NET Core](https://github.com/aspnet/AspNetCore) - Interfacing of the API
### Algorithm
- [SPF](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm) - Calculation of the shortest garbage bin sequence
    - [Hand-made by yours truly](https://github.com/IanEscober/WasteMan/blob/master/src/WasteMan.Algorithm/Core/ShortestPathFirst.cs)
- [DFS](https://en.wikipedia.org/wiki/Depth-first_search) - Discovery of the shortest collection route
    - [Hand-made by yours truly](https://github.com/IanEscober/WasteMan/blob/master/src/WasteMan.Algorithm/Core/ModifiedDepthFirstSearch.cs)

## Requirements
- [MongoDB](https://www.mongodb.com/download-center/community) - Any version will do, preferably the latest
- [Redis](https://redis.io/download) - Windows: either use docker or [this](https://github.com/microsoftarchive/redis/releases)
- [.NET Core](https://dotnet.microsoft.com/) - 2.1 or higher (due to SignalR dependecy)

## Installation/Operation
[WasteMan Wiki](https://github.com/IanEscober/WasteMan/wiki)

## Contribution
Yeet a Pull Request

## License
[MIT](https://github.com/IanEscober/WasteMan/blob/master/License)