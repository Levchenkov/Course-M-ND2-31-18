/*$(document).ready(function () {

    $(document).ready(function () {

        // See the html on the View below
        $("#send").click(function () {
            var addModel = {
                AddedTags: $('#AddedTags').val(),
                AddedHeader: $('#AddedHeader').val(),
                AddedTwit: $('#AddedTwit').val()
            };

            $.ajax({
                type: 'POST',
                url: '/twit/add',
                data: addModel,
                success: function (data) {
                    //Do Stuff 
                    console.log(data)
                },
                error: function (data) {
                    console.log(data)
                }
            });

        });
   
    });
});*/

$(function () {

    let hubUrl = '/hubs';
    let httpConnection = new signalR.HttpConnection(hubUrl);
    let hubConnection = new signalR.HubConnection(httpConnection);

 
    hubConnection.on('OnReportPublished', 
        (header, tags, twit) => {

            $('#reports').prepend($('<li>').
                append($('<div class="card border">').
                    append($('<div class="card-header h2 text-light bg-dark">').text(header).
                        add($('<div class="card-body h3 ">').text(twit).
                            add($('<div class="card-footer h6 text-primary">').text(tags))))));
               
    });
    hubConnection.start();

});

$(function () {

    let hubUrl = '/hubs';
    let httpConnection = new signalR.HttpConnection(hubUrl);
    let hubConnection = new signalR.HubConnection(httpConnection);

    $("#send").click(function () {
        hubConnection.invoke('PublishReport', $('#AddedHeader').val(),
            $('#AddedTags').val(), $('#AddedTwit').val());
    });

    hubConnection.start();

});

