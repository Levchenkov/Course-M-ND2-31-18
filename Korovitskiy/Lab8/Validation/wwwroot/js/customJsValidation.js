jQuery.validator.addMethod("creditcard", function (value, element) {
    if (value < 999999999999999 || value > 10000000000000000) {
        return false;
    }

    var arrayInt = value.split('');
    for (var i = 15; i >= 0; i--) {
        if (i % 2 != 1) {
            var digitX2 = arrayInt[i] * 2;
            arrayInt[i] = digitX2 > 9 ? digitX2 - 9 : digitX2;
        }
        else {
            arrayInt[i] = +arrayInt[i];
        }
    }
    var sum = arrayInt.reduce(function (sum, current) {
        return sum + current;
    }, 0);
    return sum % 10 == 0;
});

jQuery.validator.unobtrusive.adapters.add("creditcard", function (options) {
    options.rules["creditcard"] = true;
    if (options.message) {
        options.messages["creditcard"] = options.message;
    }
});

jQuery.validator.addMethod("expirationyear", function (value, element) {
    var date = new Date();
    return value >= date.getFullYear();
});

jQuery.validator.unobtrusive.adapters.add("expirationyear", function (options) {
    options.rules["expirationyear"] = true;
    if (options.message) {
        options.messages["expirationyear"] = options.message;
    }
});

jQuery.validator.addMethod("expirationmonth", function (value, element) {
    
    if (value < 1 || value > 12) {
        return false;
    }

    var now = new Date();
    var inputDate = new Date($('#ExpirationYear').val(), value, now.getDate());
    return inputDate >= now;
});

jQuery.validator.unobtrusive.adapters.add("expirationmonth", function (options) {
    options.rules["expirationmonth"] = true;
    if (options.message) {
        options.messages["expirationmonth"] = options.message;
    }
}); 

jQuery.validator.addMethod("securitycode", function (value, element) {

    if (value < 100 || value > 999) {
        return false;
    }
    return true;
});

jQuery.validator.unobtrusive.adapters.add("securitycode", function (options) {
    options.rules["securitycode"] = true;
    if (options.message) {
        options.messages["securitycode"] = options.message;
    }
}); 

jQuery.validator.addMethod("amount", function (value, element) {

    if (value < 0.01 && value > 99999.99) {
        return false;
    }
    return true;
});

jQuery.validator.unobtrusive.adapters.add("amount", function (options) {
    options.rules["amount"] = true;
    if (options.message) {
        options.messages["amount"] = options.message;
    }
}); 

jQuery.validator.addMethod("postcode", function (value, element) {

    if (value < 10000 && value > 99999) {
        return false;
    }
    return true;
});

jQuery.validator.unobtrusive.adapters.add("postcode", function (options) {
    options.rules["postcode"] = true;
    if (options.message) {
        options.messages["postcode"] = options.message;
    }
}); 