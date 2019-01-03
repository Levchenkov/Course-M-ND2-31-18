
var tokenKey = "accessToken";
var link = 'https://localhost:44368/';
var linkData = 'https://localhost:44368/data/getusers';
var linkReg = 'https://localhost:44368/authorize/Register'; 

$(document).ajaxError(function (args, data) {
    if (data.status == 401) {
        window.location.href = link;
    };
});

$(document).ajaxSend(function (eventArg, xhr) {
    var token = sessionStorage.getItem(tokenKey);
    xhr.setRequestHeader("Authorization", "Bearer " + token);
});

$('#signOff').click(function (e) {
    e.preventDefault();
    sessionStorage.removeItem(tokenKey);
    $('#auth').show();
    $('#users').hide();
});


$('form').submit(function (e) {
    e.preventDefault();
    var $form = $(this);
    $.ajax({
        type: $form.attr('method'),
        url: $form.attr('action'),
        data: $form.serialize()
    }).success(function (data) {
        sessionStorage.setItem(tokenKey, data.access_token);
        $('#auth').hide();
    }).fail(function () {
        console.log('fail');
    });
});

$('#showUser').click(function (data) {
    debugger;
    $.ajax({
        type: 'GET',
        url: linkData,
        success: function (data) {
            $('#users').html(data);
            $('#users').show();
        }
    })
})

$('#register').click(function (data) {
    window.location.href = linkReg;
});





