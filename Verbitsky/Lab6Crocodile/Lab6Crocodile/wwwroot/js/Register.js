function Register() {
    $('#spn-name').text($('#name').val());
    $('#entrance').hide();
    $('#chat').show();
    $('#myCanvas').show();
    connection.invoke("Register", $('#name').val(), $('#dropDownList').val());
}