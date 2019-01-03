var connectForm = document.getElementById("connect-form");
var sendForm = document.getElementById("send-form");
var messagesList = document.getElementById("messages-list");
var messageTextBox = document.getElementById("message-textbox");
var canvas = document.getElementById("myCanvas"),
    context = canvas.getContext("2d"),
    w = canvas.width,
    h = canvas.height;
var mouse = { x: 0, y: 0 };
var draw = false;

var connection = new signalR.HubConnectionBuilder()
    .withUrl('/crocodile')
    .build();

connection.start();

function appendMessage(content, isRightWord) {
    var li = document.createElement("li");
    li.innerText = content;
    if (isRightWord) {
        li.style.color = "green";
    }
    messagesList.appendChild(li);
}


connectForm.addEventListener("submit", function(evt) {
    connection.invoke("Connect", $('#name').val());
    $('#connect-form').hide();
    $('#chat').show();
    $('#myCanvas').show();
    $('#myCanvas').css("pointer-events", "none");
    evt.preventDefault();
});

sendForm.addEventListener("submit", function (evt) {
    var message = messageTextBox.value;
    messageTextBox.value = "";
    connection.send("Send" ,message);
    evt.preventDefault();
});

connection.on("Send", function (sender, message, isRightWord) {
    appendMessage(sender + ': ' + message, isRightWord);
});

connection.on("BeginGame",
    function(secretWord) {
            $('#myCanvas').css("pointer-events", "auto");
            $("#SecretWord").text("Secret word: " + secretWord);
        $("#SecretWord").show();
    });

connection.on("EndGame",
    function () {
        $('#myCanvas').css("pointer-events", "none");
        context.clearRect(0, 0, canvas.width, canvas.height);
        $("#SecretWord").empty();
    });


connection.on("MouseDown", function (x, y) {
    draw = true;
    context.beginPath();
    context.moveTo(x, y);
});

connection.on("MouseMove", function (x, y) {
    context.lineTo(x, y);
    context.stroke();
});

connection.on("MouseUp", function (x, y) {
    context.lineTo(x, y);
    context.stroke();
    context.closePath();
    draw = false;
});

canvas.addEventListener("mousedown", function (e) {
    mouse.x = e.pageX - this.offsetLeft;
    mouse.y = e.pageY - this.offsetTop;
    draw = true;
    connection.invoke("MouseDown", mouse.x, mouse.y);
});

canvas.addEventListener("mousemove", function (e) {
    if (draw === true) {
        mouse.x = e.pageX - this.offsetLeft;
        mouse.y = e.pageY - this.offsetTop;
        connection.invoke("MouseMove", mouse.x, mouse.y);
    }
});

canvas.addEventListener("mouseup", function (e) {
    mouse.x = e.pageX - this.offsetLeft;
    mouse.y = e.pageY - this.offsetTop;
    draw = false;
    connection.invoke("MouseUp", mouse.x, mouse.y);
});


