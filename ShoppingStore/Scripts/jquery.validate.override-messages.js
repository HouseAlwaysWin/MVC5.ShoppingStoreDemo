
var overrideErrorMS = function () {


    //console.log(Resources["RequiredField"]);


    var message = $.validator.messages;

    message.required = String(Resources["RequiredField"]);
    message.remote = Resources["RemoteError"];
    message.email = Resources["EmailError"];
    message.url = Resources["UrlError"];
    message.date = Resources["DateError"];
    message.dateISO = Resources["DateISOError"];
    message.number = Resources["NumberError"];
    message.digits = Resources["DigitsOnlyError"];
    message.creditcard = Resources["CreditCardError"];
    message.equalTo = Resources["PasswordConfirmError"];
    message.accept = Resources["AcceptError"];
    message.maxlength = jQuery.validator.format(Resources["MaxLengthError"]);
    message.minlength = jQuery.validator.format(Resources["MinLengthError"]);
    message.rangelength = jQuery.validator.format(Resources["RangeLengthError"]);
    message.range = jQuery.validator.format(Resources["RangeError"]);
    message.max = jQuery.validator.format(Resources["MaxError"]);
    message.min = jQuery.validator.format(Resources["MinError"]);



};

