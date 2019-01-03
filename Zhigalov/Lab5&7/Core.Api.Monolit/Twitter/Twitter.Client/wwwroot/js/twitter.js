$("#submitTweet").click(function () {
    $.ajax({
        type: 'POST',
        data: {
            content: $("#newTweet").val()
        },
        url: 'http://localhost:59125/api/twitter',
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem("accessToken");
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function () {
            document.location.href = document.location.href;
        },
        fail: function (data) {
            console.log(data);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:59125/api/twitter',
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem("accessToken");
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (tweets) {
            $('.card-title').text(sessionStorage.getItem("userName"));
            var container = $("#tweets");
            tweets.forEach(function (item) {
                container.append(
                    '<a href="#" class="mt-2 list-group-item list-group-item-action flex-column align-items-start">' +
                    '<div class="d-flex w-100 justify-content-between">' +
                    '<h5 class="mb-1">' + item.authorName + '</h5>' +
                    '<small class="text-muted">' + item.date + '</small></div>' +
                    '<p class="mb-1">' + item.content + '</p></a>');
            });
        },
        fail: function (data) {
            console.log(data);
        }
    });
});

$('#logOut').click(function (e) {
    e.preventDefault();
    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("userName");
    window.location.href = "http://localhost:49720/login.html";
});

$.ajaxSetup({
    error: function (xhr, status, err) {
        if (xhr.status == 401)
            window.location.href = "http://localhost:49720/login.html";
    }
})