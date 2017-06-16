$(function () {

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

    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
        var username = $('#username').val();

        var password = $('#password').val();



        var loginModel = {
            "grant_type": "password",
            username: username,
            password: password
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






    $('#registerForm').on("submit", function (e) {
        e.preventDefault();

        $('#registerModal').on('hidden.bs.modal', '.modal-body', function () {
            $(this).removeData('bs.modal');
        });
        var username = $('#register-username').val();
        var email = $('#register-email').val();
        var password = $('#register-password').val();
        var passwordConfirm = $('#register-passwordConfirm').val();

        var registerModel = {
            "UserName": username,
            "Email": email,
            "Password": password,
            "ConfirmPassword": passwordConfirm
        };




        var summaryValid = $('#summary-valid');
        var usernameValid = $('#register-username-valid');
        var emailValid = $('#register-email-valid');
        var passwordValid = $('#register-password-valid');
        var passwordConfirmValid = $('#register-passwordconfirm-valid');



        $.ajax({
            url: "api/account/register",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(registerModel)
        }).done(function (data) {
            alert("register Successful");

        }).fail(function (jqXHR, textStatus, data) {

            var error = $.parseJSON(jqXHR.responseText);
            var emailError = error.modelState["model.Email"];
            var usernameError = error.modelState["model.UserName"];
            var passwordError = error.modelState["model.Password"];
            var passwordConfirmError = error.modelState["model.ConfirmPassword"];


            var summaryError = error.modelState;


            if (summaryError) {
                $.each(summaryError, function (index, value) {
                    summaryValid.text(value).css("color", "red");
                });
            } else {
                summaryValid.text("");
            }


            if (usernameError) {
                $.each(usernameError, function (index, value) {
                    usernameValid.text(value).css("color", "red");

                });
            } else {
                usernameValid.text("");
            }

            if (emailError) {
                $.each(emailError, function (index, value) {
                    emailValid.text(value).css("color", "red")
                });
            }
            else {
                emailValid.text("");
            }

            if (passwordError) {
                $.each(passwordError, function (index, value) {
                    passwordValid.text(value).css("color", "red");
                });
            } else {
                passwordValid.text("");
            }


            if (passwordConfirmError) {
                $.each(passwordConfirmError, function (index, value) {
                    passwordConfirmValid.text(value).css("color", "red");
                });
            } else {
                passwordConfirmValid.text("");
            }

            console.log("Register Failed");
        })
    });
});