let hubUrl = 'https://localhost:44336/Hub';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();
let userName = '';

hubConnection.on('Send', function (message, userName) {
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(userName + ': '));

    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

hubConnection.on('UpdatePlayers', function (players) {
    document.getElementById("players").innerHTML = '';
    for (var i = 0; i < players.length; i++) {
        let userNameElem = document.createElement("b");
        userNameElem.appendChild(document.createTextNode(players[i].name));

        let elem = document.createElement("p");
        elem.appendChild(userNameElem);

        var firstElem = document.getElementById("players").firstChild;
        document.getElementById("players").insertBefore(elem, firstElem);

        if (players[i].master & players[i].name == document.getElementById("userName").innerText) {
            document.getElementById("word").innerHTML = "<p><h3>" + players[i].word + "</h3><p>"
        }
    }
});

document.getElementById("loginBtn").addEventListener("click", function (e) {
    userName = document.getElementById("userName").innerText;
    $('#loginBtn').css('display', 'none');
    $('#inputForm').css('display', 'block');
    $('#crocoCanvas').css('display', 'block');
    hubConnection.invoke('Connect', userName);
});

document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    document.getElementById('message').value = '';
    hubConnection.invoke('Send', message, userName);
});

hubConnection.start();

var canvas = document.getElementById("crocoCanvas"),
    context = canvas.getContext("2d");

var mouse = { x: 0, y: 0 };
var draw = false;

hubConnection.on("MouseDown", function (x, y) {
    draw = true;
    context.beginPath();
    context.moveTo(x, y);
});
hubConnection.on("MouseMove", function (x, y) {
    context.lineTo(x, y);
    context.stroke();
});
hubConnection.on("MouseUp", function (x, y) {
    context.lineTo(x, y);
    context.stroke();
    context.closePath();
    draw = false;
});

canvas.addEventListener("mousedown", function (e) {
    var rect = canvas.getBoundingClientRect();
    mouse.x = e.pageX - rect.left;
    mouse.y = e.pageY - rect.top;
    draw = true;
    hubConnection.invoke("MouseDown", mouse.x, mouse.y);
});
canvas.addEventListener("mousemove", function (e) {
    if (draw == true) {
        var rect = canvas.getBoundingClientRect();
        mouse.x = e.pageX - rect.left;
        mouse.y = e.pageY - rect.top;
        hubConnection.invoke("MouseMove", mouse.x, mouse.y);
    }
});
canvas.addEventListener("mouseup", function (e) {
    var rect = canvas.getBoundingClientRect();
    mouse.x = e.pageX - rect.left;
    mouse.y = e.pageY - rect.top;
    draw = false;
    hubConnection.invoke("MouseUp", mouse.x, mouse.y);
});
