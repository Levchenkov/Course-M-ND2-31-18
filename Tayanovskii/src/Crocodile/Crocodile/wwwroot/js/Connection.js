var connection = new signalR.HubConnectionBuilder()
    .withUrl("/crocodile")
    .configureLogging(signalR.LogLevel.Information)
    .build();