$(function () {
    overrideErrorMS();
    loginFormFunc();
    registerFormFunc();
    sendEmailConfirm();
    modalChangeGroup();
    resetEmail();

});


var modalChanger = function (button, dismissModal) {
    button.on('click', function (e) {
        e.preventDefault();
        dismissModal.modal('toggle');
        dismissModal.on('hidden.bs.modal', function () {
            $('body').addClass('modal-open');
        });
    });
};


var modalChangeGroup = function () {
    // modals
    var loginmodal = $('#loginModal');
    var emailmodal = $('#sendEmailModal');
    var registermodal = $('#registerModal');

    // login page btn
    var registerBtn = $('#login-register');
    var emailBtn = $('#no-received-email');
    var forgotBtn = $('#forgot-password');

    // email page btn
    var loginBackBtn = $('#email_goback');

    // register page btn
    var loginBtn = $('#register-login');

    // login changer
    modalChanger(registerBtn, loginmodal);
    modalChanger(emailBtn, loginmodal);
    modalChanger(forgotBtn, loginmodal);

    // register changer
    modalChanger(loginBtn, registermodal);

    // email changer
    modalChanger(loginBackBtn, emailmodal);
};

var sendEmailConfirm = function () {
    var form = $('#emailForm');

    var forgotBtn = $('#forgot-password');
    forgotBtn.click(function (e) {
        e.preventDefault();
        form.addClass("forgot").removeClass("noEmail");
    });

    var noReceivedBtn = $('#no-received-email');
    noReceivedBtn.click(function (e) {
        e.preventDefault();
        form.addClass("noEmail").removeClass("forgot");
    });

    form.on('submit', function (e) {
        e.preventDefault();
        var email = $('#send_email').val();
        var model = {
            Email: $('#send_email').val()
        };

        var url;
        if (form.hasClass("forgot")) {
            url = "Account/ForgotPassword";
        } else if (form.hasClass("noEmail")) {
            url = "Account/SendVerifiedEmail";
        }
        sendEmailAjax(url, model);
    });
};


var sendEmailAjax = function (sendUrl, email) {
    $.ajax({
        url: sendUrl,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(email),
        dataType: "json"
    }).done(function (data) {
        console.log(data);
    }).fail(function (jqXHR, statusText, data) {
    });
};


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
        var returnUrl = document.location.origin + "/Account/ConfirmEmail?";
        console.log(returnUrl);
        var model = {
            "UserName": $('#register-username').val(),
            "Email": $('#register-email').val(),
            "Password": $('#register-password').val(),
            "ConfirmPassword": $('#register-passwordConfirm').val(),
            "ReturnUrl": returnUrl
        };

        var summaryMessage = $('#register-error-message');

        $.ajax({
            url: "api/account/register",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(model)
        }).done(function (data) {
            $('#registerModal').modal('toggle');
            $('#register-success').modal('toggle');

            sendEmailAjax(data);
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

var resetEmail = function () {
    var form = $('#reset-email-form');

    form.validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            NewPassword: {
                required: true,
                minlength: 6
            },
            ConfirmPassword: {
                equalTo: "#reset_newpassword"
            }

        }
    });
}