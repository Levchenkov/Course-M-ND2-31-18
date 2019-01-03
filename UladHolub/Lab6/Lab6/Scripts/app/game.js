$(function () {

    $('#launchMenu').hide();
    $('#gameContainer').hide();
    $('#loginContainer').show();

    var indexhub = $.connection.gameHub;

    indexhub.client.addMessage = function (name, message) {
        $('#chatBox').append('<p class="m-0"><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '</p>');
        $('#chatBox').scrollTop($('#chatBox').prop("scrollHeight"));
    };

    indexhub.client.addConnectMessage = function (name, message) {
        AddConnectMessage(name, message);
    };

    indexhub.client.addLeaderMessage = function (message) {
        AddLeaderMessage(message);
    };

    indexhub.client.onConnected = function (currentUser, allUsers, allGroups) {

        $('#loginContainer').hide();
        $('#gameContainer').hide();
        $('#launchMenu').show();

        $('#userId').val(currentUser.Id);
        $('#userName').val(currentUser.Name);
        $('#header').html('<h3>Welcome, ' + currentUser.Name + '</h3>');

        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i]);
        }

        for (i = 0; i < allGroups.length; i++) {

            AddGroupToMenu(allGroups[i]);

            $("#btnConnect" + allGroups[i].Id).click(function () {

                indexhub.server.connectToRoom($(this).data("id"));
            });
        }
    };

    indexhub.client.onNewUserConnected = function (user) {

        AddUser(user);
    };

    indexhub.client.onUserDisconnected = function (user) {

        Remove(user.Id);
    };

    indexhub.client.onNewGroupUserConnected = function (user) {

        AddGroupUser(user);
    };

    indexhub.client.onGroupUserDisconnected = function (user) {

        Remove('groupUser'+user.Id);
    };

    indexhub.client.onGroupConnected = function (user, group) {

        $('#loginContainer').hide();
        $('#launchMenu').hide();
        $('#gameContainer').show();

        $('#userId').val(user.Id);
        $('#userName').val(user.Name);
        $('#groupName').val(group.Id);

        $('#btnReturnToMenu').hide();
        $('#gameCanvas').empty();
        $('#groupUsers').empty();
        $('#chatBox').empty();

        if (group.Leader !== null) {
            $('#groupLeader').val(group.Leader.Id);
        }

        for (i = 0; i < group.Users.length; i++) {

            AddGroupUser(group.Users[i]);
        }
    };

    indexhub.client.addGroupToMenu = function (group) {

        AddGroupToMenu(group);

        $("#btnConnect" + group.Id).click(function () {

            indexhub.server.connectToRoom($(this).data("id"));
        });
    };

    indexhub.client.removeGroupFromMenu = function (id) {

        Remove(id);
    };

    indexhub.client.changeGroupCounter = function (group) {

        ChangeGroupCounter(group);
    };

    indexhub.client.startGame = function (group) {
        if (group.Leader !== null) {
            $('#groupLeader').val(group.Leader.Id);
        }
        AddConnectMessage("", "game started!");
        AddConnectMessage(group.Leader.Name, "- group leader!");
    };

    indexhub.client.endGame = function (group) {
        $('#btnReturnToMenu').show();
        $('#groupName').val("");
        $('#groupLeader').val("");
        indexhub.server.endGame(group.Id);
    };


    var lastx;
    var lasty;
    var mouseDown = false;
    var context = $("#gameCanvas")[0].getContext("2d");

    indexhub.client.mouseDown = function (x, y) {
        MouseDown(x, y);
    };

    indexhub.client.mouseMove = function (x, y) {
        MouseMove(x, y);
    };

    indexhub.client.mouseUp = function () {
        MouseUp();
    };

    indexhub.client.mouseLeave = function () {
        MouseLeave();
    };

    function MouseDown(x, y) {
        lastx = x;
        lasty = y;
        mouseDown = true;
    }

    function MouseMove(x, y) {
        if (mouseDown) {
            context.beginPath();
            context.moveTo(lastx, lasty);
            context.lineTo(x, y);
            context.stroke();
            lastx = x;
            lasty = y;
        }
    }

    function MouseUp() {
        mouseDown = false;
    }

    function MouseLeave() {
        $("#gameCanvas").mouseup();
    }

    $.connection.hub.start().done(function () {

        $("#btnLogin").click(function () {

            var name = $("#loginName").val();
            if (name.length > 0) {
                indexhub.server.connect(name);
            }
            else {
                alert("Please enter your name!");
            }
        });

        $("#btnCreateRoom").click(function () {

            var number = $("#playersNumber").val();
            indexhub.server.createRoom(number);
        });

        $("#btnSendMessage").click(function () {

            if ($('#groupLeader').val() === $('#userId').val()) { return; }
            var message = $("#chatMessage").val();
            if (message === '') { return; }
            var group = $("#groupName").val();
            var name = $("#userName").val();
            $("#chatMessage").val('');
            indexhub.server.sendMessage(message, name, group);
        });

        $("#btnReturnToMenu").click(function () {
            
            $('#loginContainer').hide();
            $('#gameContainer').hide();
            $('#launchMenu').show();
        });

        $("#gameCanvas").mousedown(function (e) {
            if ($('#groupLeader').val() === "" || $('#groupLeader').val() !== $('#userId').val()) { return; }
            indexhub.server.mouseDown($('#groupName').val(), e.offsetX, e.offsetY);
            MouseDown(e.offsetX, e.offsetY);
        }).mousemove(function (e) {
            if ($('#groupLeader').val() === "" || $('#groupLeader').val() !== $('#userId').val()) { return; }
            indexhub.server.mouseMove($('#groupName').val(), e.offsetX, e.offsetY);
            MouseMove(e.offsetX, e.offsetY);
        }).mouseup(function () {
            if ($('#groupLeader').val() === "" || $('#groupLeader').val() !== $('#userId').val()) { return; }
            indexhub.server.mouseUp($('#groupName').val());
            MouseUp();
        }).mouseleave(function () {
            if ($('#groupLeader').val() === "" || $('#groupLeader').val() !== $('#userId').val()) { return; }
            indexhub.server.mouseLeave($('#groupName').val());
            MouseLeave();
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function Remove(id) {
    $('#' + id).remove();
}


function AddUser(user) {

    var userId = $('#userId').val();

    if (userId !== user.Id) {

        $("#users").append('<p class="mb-0" id="' + user.Id + '"><b>' + user.Name + '</b></p>');
    }
}

function AddGroupUser(user) {

    var userId = $('#groupUserId').val();
    $("#groupUsers").append('<p class="mb-0" id="groupUser' + user.Id + '"><b>' + user.Name + '</b></p>');
}

function AddGroupToMenu(group) {

    $("#groupMenu").append(
        '<div id="' + group.Id
        + '" class="container mt-1 p-1 rounded bg-white text-dark">'
        + '<input id="btnConnect' + group.Id + '" ' + 'data-id="' + group.Id
        + '" class="btn btn-sm btn-dark my-0" type="button" value="Connect" />'
        + ' Group: ' + group.Id
        + ' <strong> Players ' + '<strong id="counter' + group.Id + '">'
        + group.Users.length + '</strong>' + '/' + group.NumberOfPlayers + '</strong>'
        + '</div >');
}

function ChangeGroupCounter(group) {
    $('#counter' + group.Id).text(group.Users.length);
}

function AddConnectMessage(name, message) {
    $('#chatBox').append('<p class="m-0 text-center"><b>' + htmlEncode(name)
        + ' ' + htmlEncode(message) + '</b></p>');
    $('#chatBox').scrollTop($('#chatBox').prop("scrollHeight"));
}

function AddLeaderMessage(message) {
    $('#chatBox').append('<p class="m-0 text-center text-success"><b>'
        + message + '</b></p>');
    $('#chatBox').scrollTop($('#chatBox').prop("scrollHeight"));
}

