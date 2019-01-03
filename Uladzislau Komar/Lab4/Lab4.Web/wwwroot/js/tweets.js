"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tweets").build();

connection.on("Send", function (user, content, created) {
    var msg = content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " tweets: " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("submitButton").addEventListener("click", function (event) {
    var user = document.getElementById("username").value;
    var content = document.getElementById("content").value;
    var created = Date.UTC(Date.now);
    connection.invoke("Send", user, content, created).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});