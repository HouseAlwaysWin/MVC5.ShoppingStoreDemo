$(function () {

    overrideErrorMS();
    loginFormFunc();
    registerFormFunc();

});


var loginFormFunc = function () {

    var form = $('#loginForm');

    $('#logoutButton').on('click', function (e) {
        $.ajax({
            url: "api/account/logout",
            type: "POST",
        }).done(function () {
            location.reload();
        }).fail(function () {
            alert("Log out failed");
        });
    });

    form.on("submit", function (e) {
        e.preventDefault();

        var loginModel = {
            "grant_type": "password",
            username: $('#login-username').val(),
            password: $('#login-password').val()
        };

        $.ajax({
            url: "/token",
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: loginModel,
        })
        .done(function (data) {
            $.ajaxSetup({ headers: "bearer " + data.access_token });
            $.ajax({
                headers: "bearer " + data.access_token
            }).done(function () {
                $('#loginModal').modal('toggle');
                location.reload();
            });
            console.log(data.access_token);
            console.log("Login success");
        })
        .fail(function (jqXHR, textStatus, data) {
            var error = $.parseJSON(jqXHR.responseText);
            $('#login-error-message').html('<h5>' + error.error_description + '</h5>').css("color", "red");
            console.log("Login Failed");
        });
    });

    form.validate({
        rules: {
            username: {
                required: true,
                minlength: 5,
            },
            password: {
                required: true,
                minlength: 5
            }
        }
    });
}



var registerFormFunc = function () {

    var form = $('#registerForm');
    form.on("submit", function (e) {
        e.preventDefault();

        // Refresh modal after submit
        $('#registerModal').on('hidden.bs.modal', '.modal-body', function () {
            $(this).removeData('bs.modal');
        });

        // Binding text content
        var registerModel = {
            "UserName": $('#register-username').val(),
            "Email": $('#register-email').val(),
            "Password": $('#register-password').val(),
            "ConfirmPassword": $('#register-passwordConfirm').val()
        };

        var summaryValid = $('#summary-valid');

        $.ajax({
            url: "api/account/register",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(registerModel)
        }).done(function (data) {
            $('#registerModal').modal('toggle');
            location.reload();

        }).fail(function (jqXHR, textStatus, data) {
            var error = $.parseJSON(jqXHR.responseText);
            var summaryError = error.modelState;
            if (summaryError) {
                $.each(summaryError, function (index, value) {
                    summaryValid.text(value).css("color", "red");
                });
            } else {
                summaryValid.text("");
            }
            console.log("Register Failed");
        })
    });

    form.validate({
        rules: {
            username: {
                required: true,
                minlength: 5
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 5
            },
            password_confirm: {
                equalTo: "#register-password"
            }
        },
    });
}