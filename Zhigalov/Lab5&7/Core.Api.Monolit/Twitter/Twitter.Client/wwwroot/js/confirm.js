$.ajaxSetup({
    success: function (xhr, status, err) {
        $('#successMessage').text(xhr.responseText);
        $('#success').css('display', 'block');
    },
    error: function (xhr, status, err) {
        $('#danger').css('display', 'block');
        if (xhr.status == 400) {
            $('#errorMessage').text(xhr.responseText);
        }
        else
            $('#errorMessage').text('Some troubles');
    }
});

$(document).ready(function () {
    //var query = parseURLParams(window.location.search);
    $.ajax({
        type: 'GET',
        url: 'http://localhost:59125/api/confirm' + window.location.search
    });
});

//function parseURLParams(url) {
//    var queryStart = url.indexOf("?");
//    var queryEnd = url.indexOf("#") + 1 || url.length + 1;
//    var query = url.slice(queryStart, queryEnd);
//    return query;
//};