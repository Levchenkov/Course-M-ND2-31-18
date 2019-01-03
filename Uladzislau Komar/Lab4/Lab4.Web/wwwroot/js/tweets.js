"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tweets").build();

connection.on("Send", function (userId, user, content, created) {
    var msg = content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "!NEW! " + user + " (" + created + ")" + ": " + msg;
    var div = document.createElement("div");
    div.class = "row";
    div.style = "border: 1px solid skyblue";
    div.textContent = encodedMsg;
    document.getElementById("tweetList").appendChild(div);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("submitButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userId").innerHTML;
    var user = document.getElementById("userName").innerHTML;
    var content = document.getElementById("content").value;
    var currentdate = new Date();
    var datetime = (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/" +
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    var created = datetime;
    connection.invoke("Send", userId, user, content, created).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});