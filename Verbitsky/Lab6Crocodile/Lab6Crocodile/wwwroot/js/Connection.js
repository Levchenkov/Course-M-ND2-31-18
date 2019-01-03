const connection = new signalR.HubConnectionBuilder()
    .withUrl('/hub')
    .build();

connection.start().catch(err => console.error(err.toString()));

var canvas = document.getElementById("myCanvas"),
    context = canvas.getContext("2d"),
    w = canvas.width,
    h = canvas.height;

var mouse = { x: 0, y: 0 };
var draw = false;

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
    if (draw == true) {
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

connection.on('Send', (name, message) => {
    let nameElement = document.createElement('strong');
    nameElement.innerText = `${name}:`;

    let msgElement = document.createElement('em');
    msgElement.innerText = ` ${message}`;

    let li = document.createElement('li');
    li.appendChild(nameElement);
    li.appendChild(msgElement);

    $('#messages').append(li);
});

connection.on('GetSecretWord', (word) => {
    $("#SecretWord").text($("#SecretWord").text() + word);
    $("#SecretWord").show();
});

document.getElementById('frm-send-message').addEventListener('submit', event => {
    let name = $('#spn-name').text();
    let message = $('#message').val();

    $('#message').val('');

    connection.invoke('Send', name, message);
    event.preventDefault();
});