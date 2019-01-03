$().ready(function () {

    $("#Amount").inputmask("9{1,5}.99");
    $("#CreditCardNumber").inputmask("9{16}");
    $("#ExpirationMonth").inputmask("9{2}");
    $("#ExpirationYear").inputmask("9{2}");
    $("#SecurityCode").inputmask("9{3}");

    $.validator.addMethod(
        "amountValidate",
        function (value, element) {
            return parseFloat(value) >= 0.01 && parseFloat(value) <= 99999.99;
        },
        "* Amount must be greater than 0.01 and must be less than 99999.99"
    );

    $.validator.addMethod(
        "dateValidate",
        function (value, element) {
            var month = $("#ExpirationMonth").val();
            var year = '20' + $("#ExpirationYear").val();
            if (month === '' || year === '') {
                return false;
            }
            var date = new Date(year, month);
            return date > new Date();
        },
        "Date must be greater than current"
    );

    $.validator.addMethod(
        "creditCardNumberValidate",
        function (value, element) {
            if (value.length !== 16) {
                return false;
            }
            var sum = 0;
            for (i = 0; i < value.length; i++) {
                if (i % 2 === 0) {
                    sum += (parseInt(value[i]) * 2) % 9;
                } else {
                    sum += parseInt(value[i]);
                }
            }
            return sum % 10 === 0;
        },
        "* Incorrect credit card number"
    );

    $("#payment").validate({
        rules: {
            FirstName: {
                required: true,
                minlength: 2
            },
            MiddleName: {
                required: true,
                minlength: 2
            },
            LastName: {
                required: true,
                minlength: 2
            },
            Address: {
                required: true,
                regex: /[\w\d\s,\.\-\\\/]+/
            },
            City: {
                required: true,
                regex: /[A-Za-z\s\-]+/
            },
            Country: {
                required: true,
                regex: /[A-Za-z\s\-]+/
            },
            PostCode: {
                required: true,
                regex: /[\d]{6}/
            },
            Email: {
                required: true,
                email: true
            },
            Amount: {
                required: true,
                regex: /\d{1,5}.\d{2}/,
                amountValidate: true
            },
            Description: {
                maxlength: 250
            },
            CreditCardNumber: {
                required: true,
                regex: /\d{16}/,
                creditCardNumberValidate: true
            },
            ExpirationMonth: {
                required: true,
                regex: /[0-1]\d/,
                dateValidate: true
            },
            ExpirationYear: {
                required: true,
                regex: /\d{2}/,
                dateValidate: true
            },
            SecurityCode: {
                required: true,
                regex: /\d{3}/
            }
        },

        messages: {
            FirstName: {
                required: "Please enter your first name",
                minlength: "Your first name must consist of at least 2 characters"
            },
            MiddleName: {
                required: "Please enter your middle name",
                minlength: "Your middle name must consist of at least 2 characters"
            },
            LastName: {
                required: "Please enter your last name",
                minlength: "Your last name must consist of at least 2 characters"
            },
            Address: {
                required: "Please enter your address",
                regex: "The address has invalid characters"
            },
            City: {
                required: "Please enter your city",
                regex: "The city has invalid characters"
            },
            Country: {
                required: "Please enter your country",
                regex: "The country has invalid characters"
            },
            PostCode: {
                required: "Please enter your post code",
                regex: "Post code represents 6-digit code like 111111"
            },
            Email: {
                required: "Please enter your email",
                email: "Enter valid email like user@example.com"
            },
            Amount: {
                required: "Please enter amount",
                regex: "Amount can only be a number like 99999.99",
                amountValidate: "Amount must be greater than 0.01 and must be less than 99999.99"
            },
            Description: {
                maxlength: "Description max length is 250 symbols"
            },
            CreditCardNumber: {
                required: "Please enter credit card number",
                regex: "Credit card number represents 16-digit number like 1234123412341234",
                creditCardNumberValidate: "* Incorrect credit card number"
            },
            ExpirationMonth: {
                required: "Please enter expiration month",
                regex: "Expiration month represents 2-digit number like 12",
                dateValidate: "Date must be greater than current"
            },
            ExpirationYear: {
                required: "Please enter expiration year",
                regex: "Expiration year represents 2-digit number like 12",
                dateValidate: "Date must be greater than current"
            },
            SecurityCode: {
                required: "Please enter security code",
                regex: "Security code represents 3-digit code like 123"
            }
        },

        errorPlacement: function (error, element) {
            var n = element.attr("id");
            error.appendTo($('#' + n + '_validationMessage'));
        }
    });
});
