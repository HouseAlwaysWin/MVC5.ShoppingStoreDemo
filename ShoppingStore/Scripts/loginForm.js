$(function () {

    overrideErrorMS();
    loginFormFunc();
    registerFormFunc();

    modalChangeGroup();


});


var modalChanger = function (button, dismissModal) {
    button.on('click', function (e) {
        e.preventDefault();
        dismissModal.modal('toggle');
        dismissModal.on('hidden.bs.modal', function () {
            $('body').addClass('modal-open');
        });
    })
}


var modalChangeGroup = function () {
    // modals
    var loginmodal = $('#loginModal');
    var emailmodal = $('#sendEmailModal');
    var registermodal = $('#registerModal');

    // login page btn
    var registerBtn = $('#login-register');
    var emailBtn = $('#sendEmailInput');

    // email page btn
    var loginBackBtn = $('#email_goback');

    // register page btn
    var loginBtn = $('#register-login');

    // login changer
    modalChanger(registerBtn, loginmodal);
    modalChanger(emailBtn, loginmodal);

    // register changer
    modalChanger(loginBtn, registermodal);

    // email changer
    modalChanger(loginBackBtn, emailmodal);
}


var loginFormFunc = function () {

    var form = $('#loginForm');

    $('#logoutButton').on('click', function (e) {
        $.ajax({
            url: "api/account/logout",
            type: "POST"
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
            data: loginModel
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
                minlength: 6
            },
            password: {
                required: true,
                minlength: 6
            }
        }
    });
};



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

        var summaryMessage = $('#register-error-message');

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
            var summaryError = error.ModelState;
            if (summaryError) {
                $.each(summaryError, function (index, value) {
                    summaryMessage.text(value).css("color", "red");
                });
            } else {
                summaryMessage.text("");
            }
            console.log("Register Failed");
        });
    });

    form.validate({
        rules: {
            username: {
                required: true,
                minlength: 6
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6
            },
            password_confirm: {
                equalTo: "#register-password"
            }
        }
    });
};